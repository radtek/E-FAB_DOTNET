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
using System.Data.OleDb;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Data.OracleClient;
using System.Threading;

/// <summary>
/// func 的摘要描述
/// </summary>
public class funcKK
{
	public funcKK()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}
  public static void CreatePDF(string html_Path, string tool_path, string SavePath)
    {

        //string aaa = "M:\\report\\Stock_new\\wkhtmltopdf\\bin\\wkhtmltopdf.exe";
        ////string aaa = Server.MapPath(".")  + "\\wkhtmltopdf\\bin\\wkhtmltopdf.exe";

        //CreatePDF("http://vsoscar.ddns.net:8080/stocK_new/Stock_strong_volume1.aspx", aaa, Server.MapPath(".") + "\\File\\" + today_detail + ".pdf");

        
        Process _process = new Process();
        _process.StartInfo.FileName = tool_path;
        _process.StartInfo.Arguments = @"" + html_Path + " " + SavePath + "";
        _process.Start();


        while (_process.HasExited)
        {
            // Waitting 60S
            Thread.Sleep(600000);
        } 


    }

    public class TextParser
    {
        private string _text;
        private int _pos;

        public string Text { get { return _text; } }
        public int Position { get { return _pos; } }
        public int Remaining { get { return _text.Length - _pos; } }
        public static char NullChar = (char)0;

        public TextParser()
        {
            Reset(null);
        }

        public TextParser(string text)
        {
            Reset(text);
        }

        /// <summary>
        /// Resets the current position to the start of the current document
        /// </summary>
        public void Reset()
        {
            _pos = 0;
        }

        /// <summary>
        /// Sets the current document and resets the current position to the start of it
        /// </summary>
        /// <param name="html"></param>
        public void Reset(string text)
        {
            _text = (text != null) ? text : String.Empty;
            _pos = 0;
        }

        /// <summary>
        /// Indicates if the current position is at the end of the current document
        /// </summary>
        public bool EndOfText
        {
            get { return (_pos >= _text.Length); }
        }

        /// <summary>
        /// Returns the character at the current position, or a null character if we're
        /// at the end of the document
        /// </summary>
        /// <returns>The character at the current position</returns>
        public char Peek()
        {
            return Peek(0);
        }

        /// <summary>
        /// Returns the character at the specified number of characters beyond the current
        /// position, or a null character if the specified position is at the end of the
        /// document
        /// </summary>
        /// <param name="ahead">The number of characters beyond the current position</param>
        /// <returns>The character at the specified position</returns>
        public char Peek(int ahead)
        {
            int pos = (_pos + ahead);
            if (pos < _text.Length)
                return _text[pos];
            return NullChar;
        }

        /// <summary>
        /// Extracts a substring from the specified position to the end of the text
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public string Extract(int start)
        {
            return Extract(start, _text.Length);
        }

        /// <summary>
        /// Extracts a substring from the specified range of the current text
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string Extract(int start, int end)
        {
            return _text.Substring(start, end - start);
        }

        /// <summary>
        /// Moves the current position ahead one character
        /// </summary>
        public void MoveAhead()
        {
            MoveAhead(1);
        }

        /// <summary>
        /// Moves the current position ahead the specified number of characters
        /// </summary>
        /// <param name="ahead">The number of characters to move ahead</param>
        public void MoveAhead(int ahead)
        {
            _pos = Math.Min(_pos + ahead, _text.Length);
        }

        /// <summary>
        /// Moves to the next occurrence of the specified string
        /// </summary>
        /// <param name="s">String to find</param>
        /// <param name="ignoreCase">Indicates if case-insensitive comparisons
        /// are used</param>
        bool ignoreCase = false;
        public void MoveTo(string s, bool ignoreCase)
        {
            _pos = _text.IndexOf(s, _pos, ignoreCase ?
                StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
            if (_pos < 0)
                _pos = _text.Length;
        }

        /// <summary>
        /// Moves to the next occurrence of the specified character
        /// </summary>
        /// <param name="c">Character to find</param>
        public void MoveTo(char c)
        {
            _pos = _text.IndexOf(c, _pos);
            if (_pos < 0)
                _pos = _text.Length;
        }

        /// <summary>
        /// Moves to the next occurrence of any one of the specified
        /// characters
        /// </summary>
        /// <param name="chars">Array of characters to find</param>
        public void MoveTo(char[] chars)
        {
            _pos = _text.IndexOfAny(chars, _pos);
            if (_pos < 0)
                _pos = _text.Length;
        }

        /// <summary>
        /// Moves to the next occurrence of any character that is not one
        /// of the specified characters
        /// </summary>
        /// <param name="chars">Array of characters to move past</param>
        public void MovePast(char[] chars)
        {
            while (IsInArray(Peek(), chars))
                MoveAhead();
        }

        /// <summary>
        /// Determines if the specified character exists in the specified
        /// character array.
        /// </summary>
        /// <param name="c">Character to find</param>
        /// <param name="chars">Character array to search</param>
        /// <returns></returns>
        protected bool IsInArray(char c, char[] chars)
        {
            foreach (char ch in chars)
            {
                if (c == ch)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Moves the current position to the first character that is part of a newline
        /// </summary>
        public void MoveToEndOfLine()
        {
            char c = Peek();
            while (c != '\r' && c != '\n' && !EndOfText)
            {
                MoveAhead();
                c = Peek();
            }
        }

        /// <summary>
        /// Moves the current position to the next character that is not whitespace
        /// </summary>
        public void MovePastWhitespace()
        {
            while (Char.IsWhiteSpace(Peek()))
                MoveAhead();
        }
    }

    public class alarm_format
    {


        private string _trx_id;
        public string trx_id
        {
            set { _trx_id = value; }
            get { return _trx_id; }
        }
        private string _type_id;
        public string type_id
        {
            set { _type_id = value; }
            get { return _type_id; }
        }
        private string _fab_id;
        public string fab_id
        {
            set { _fab_id = value; }
            get { return _fab_id; }
        }

        private string _sys_type;
        public string sys_type
        {
            set { _sys_type = value; }
            get { return _sys_type; }
        }

        private string _eq_id;
        public string eq_id
        {
            set { _eq_id = value; }
            get { return _eq_id; }
        }
        private string _alarm_id;
        public string alarm_id
        {
            set { _alarm_id = value; }
            get { return _alarm_id; }
        }
        private string _alarm_text;
        public string alarm_text
        {
            set { _alarm_text = value; }
            get { return _alarm_text; }
        }


        private string _mail_contenttype;
        public string mail_contenttype
        {
            set { _mail_contenttype = value; }
            get { return _mail_contenttype; }
        }

        private string _alarm_comment;
        public string alarm_comment
        {
            set { _alarm_comment = value; }
            get { return _alarm_comment; }
        }


        private string _pc_ip;
        public string pc_ip
        {
            set { _pc_ip = value; }
            get { return _pc_ip; }
        }

        private string _pc_name;
        public string pc_name
        {
            set { _pc_name = value; }
            get { return _pc_name; }
        }

        private string _operator1;
        public string operator1
        {
            set { _operator1 = value; }
            get { return _operator1; }
        }


        private string _issue_date;
        public string issue_date
        {
            set { _issue_date = value; }
            get { return _issue_date; }
        }






    }


    public class Holiday_SMS
    {
        //T1ARRAY

        private string _t1a_input;
        public string t1a_input
        {
            set { _t1a_input = value; }
            get { return _t1a_input; }
        }

        private string _t1a_mtd_input;
        public string t1a_mtd_input
        {
            set { _t1a_mtd_input = value; }
            get { return _t1a_mtd_input; }
        }

        private string _t1a_output;
        public string t1a_output
        {
            set { _t1a_output = value; }
            get { return _t1a_output; }
        }

        private string _t1a_mtd_output;
        public string t1a_mtd_output
        {
            set { _t1a_mtd_output = value; }
            get { return _t1a_mtd_output; }
        }

        private string _t1a_n_cst;
        public string t1a_n_cst
        {
            set { _t1a_n_cst = value; }
            get { return _t1a_n_cst; }
        }

        private string _t1a_w_cst;
        public string t1a_w_cst
        {
            set { _t1a_w_cst = value; }
            get { return _t1a_w_cst; }
        }

        //  T1CELL

        private string _t1c_input;
        public string t1c_input
        {
            set { _t1c_input = value; }
            get { return _t1c_input; }
        }

        private string _t1c_mtd_input;
        public string t1c_mtd_input
        {
            set { _t1c_mtd_input = value; }
            get { return _t1c_mtd_input; }
        }

        private string _t1c_output;
        public string t1c_output
        {
            set { _t1c_output = value; }
            get { return _t1c_output; }
        }

        private string _t1c_mtd_output;
        public string t1c_mtd_output
        {
            set { _t1c_mtd_output = value; }
            get { return _t1c_mtd_output; }
        }

        private string _t1c_n_cst;
        public string t1c_n_cst
        {
            set { _t1c_n_cst = value; }
            get { return _t1c_n_cst; }
        }

        private string _t1c_w_cst;
        public string t1c_w_cst
        {
            set { _t1c_w_cst = value; }
            get { return _t1c_w_cst; }
        }
        // T0CELL

        private string _t0c_input;
        public string t0c_input
        {
            set { _t0c_input = value; }
            get { return _t0c_input; }
        }

        private string _t0c_mtd_input;
        public string t0c_mtd_input
        {
            set { _t0c_mtd_input = value; }
            get { return _t0c_mtd_input; }
        }

        private string _t0c_output;
        public string t0c_output
        {
            set { _t0c_output = value; }
            get { return _t0c_output; }
        }

        private string _t0c_mtd_output;
        public string t0c_mtd_output
        {
            set { _t0c_mtd_output = value; }
            get { return _t0c_mtd_output; }
        }

        private string _t0c_n_cst;
        public string t0c_n_cst
        {
            set { _t0c_n_cst = value; }
            get { return _t0c_n_cst; }
        }

        private string _t0c_w_cst;
        public string t0c_w_cst
        {
            set { _t0c_w_cst = value; }
            get { return _t0c_w_cst; }
        }

        //T1F

        private string _t1f_input;
        public string t1f_input
        {
            set { _t1f_input = value; }
            get { return _t1f_input; }
        }

        private string _t1f_mtd_input;
        public string t1f_mtd_input
        {
            set { _t1f_mtd_input = value; }
            get { return _t1f_mtd_input; }
        }

        private string _t1f_output;
        public string t1f_output
        {
            set { _t1f_output = value; }
            get { return _t1f_output; }
        }

        private string _t1f_mtd_output;
        public string t1f_mtd_output
        {
            set { _t1f_mtd_output = value; }
            get { return _t1f_mtd_output; }
        }

        private string _t1f_n_cst;
        public string t1f_n_cst
        {
            set { _t1f_n_cst = value; }
            get { return _t1f_n_cst; }
        }

        private string _t1f_w_cst;
        public string t1f_w_cst
        {
            set { _t1f_w_cst = value; }
            get { return _t1f_w_cst; }
        }

        private string _t1f_array_wip;
        public string t1f_array_wip
        {
            set { _t1f_array_wip = value; }
            get { return _t1f_array_wip; }
        }

        private string _t1f_cf_wip;
        public string t1f_cf_wip
        {
            set { _t1f_cf_wip = value; }
            get { return _t1f_cf_wip; }
        }




    }



    public void Create_Alarm_xml(string file_path, string file_name, ArrayList Element, ArrayList Element_txt)
    {
        XmlTextWriter xmlw = new XmlTextWriter(file_path + "\\" + file_name, null);
        xmlw.Formatting = Formatting.Indented;
        xmlw.Indentation = 3;
        xmlw.WriteStartDocument();
        xmlw.WriteStartElement("transaction");

        for (int i = 0; i < Element.Count; i++)
        {

            xmlw.WriteElementString(Element[i].ToString(), Element_txt[i].ToString());

        }

        xmlw.WriteEndElement();
        xmlw.Flush();
        xmlw.Close();

    }
    public static string get_netdrive_id()
     {
         string add_drive_id = "";
         string[] drives = Directory.GetLogicalDrives();

         if (drives[drives.Length - 1] == "C:\\")
             add_drive_id = "D";
         if (drives[drives.Length - 1] == "D:\\")
             add_drive_id = "E";
         if (drives[drives.Length - 1] == "E:\\")
             add_drive_id = "F";
         if (drives[drives.Length - 1] == "F:\\")
             add_drive_id = "G";
         if (drives[drives.Length - 1] == "G:\\")
             add_drive_id = "H";
         if (drives[drives.Length - 1] == "H:\\")
             add_drive_id = "I";
         if (drives[drives.Length - 1] == "I:\\")
             add_drive_id = "J";
         if (drives[drives.Length - 1] == "J:\\")
             add_drive_id = "K";
         if (drives[drives.Length - 1] == "K:\\")
             add_drive_id = "L";
         if (drives[drives.Length - 1] == "L:\\")
             add_drive_id = "M";
         if (drives[drives.Length - 1] == "M:\\")
             add_drive_id = "N";
         if (drives[drives.Length - 1] == "N:\\")
             add_drive_id = "O";
         if (drives[drives.Length - 1] == "O:\\")
             add_drive_id = "P";
         if (drives[drives.Length - 1] == "P:\\")
             add_drive_id = "Q";
         if (drives[drives.Length - 1] == "Q:\\")
             add_drive_id = "R";
         if (drives[drives.Length - 1] == "R:\\")
             add_drive_id = "S";
         if (drives[drives.Length - 1] == "S:\\")
             add_drive_id = "T";
         if (drives[drives.Length - 1] == "T:\\")
             add_drive_id = "U";

         if (drives[drives.Length - 1] == "U:\\")
             add_drive_id = "V";
         if (drives[drives.Length - 1] == "V:\\")
             add_drive_id = "W";
         if (drives[drives.Length - 1] == "W:\\")
             add_drive_id = "X";
         if (drives[drives.Length - 1] == "X:\\")
             add_drive_id = "Y";
         if (drives[drives.Length - 1] == "Y:\\")
             add_drive_id = "Z";

         if (drives[drives.Length - 1] == "Z:\\")
             add_drive_id = "A";

         return add_drive_id;

     
     }

    


 public static void start_process(string cmd_string)
 {

         Process prc = new Process(); 
prc.StartInfo.FileName = @"cmd.exe "; 
prc.StartInfo.UseShellExecute = false; 
prc.StartInfo.RedirectStandardInput = true; 
prc.StartInfo.RedirectStandardOutput = true; 
prc.StartInfo.RedirectStandardError = true; 
prc.StartInfo.CreateNoWindow = false; 

prc.Start();

string cmd = @"net use z: \\192.168.1.1\tmp pass /user:name ";
cmd = cmd_string;
prc.StandardInput.WriteLine(cmd); 
prc.StandardInput.Close();
 
 }




    public static void write_log(string program_name, string file_path, string file_type)
    {
        StreamWriter sw;
        DirectoryInfo di;//宣告目錄 
        FileInfo fi;//宣告檔案 
        string program_name1 = program_name;
        //di = new DirectoryInfo(Server.MapPath(".") + "\\RUN_LOG\\" ); //DateTime.Now.ToString("yyyyMMdd") 
        di = new DirectoryInfo(file_path); //DateTime.Now.ToString("yyyyMMdd") 
        //fi = new FileInfo(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
        fi = new FileInfo(file_path + DateTime.Now.ToString("yyyyMMdd") + "." + file_type);

        if (!di.Exists)
        {
            di.Create();//目錄不存在 產生目錄 
        }
        if (fi.Exists == true)
        {
            //檔案存在 寫檔案 
            //sw = File.AppendText(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
            sw = File.AppendText(file_path + DateTime.Now.ToString("yyyyMMdd") + "." + file_type);
        }
        else
        {
            sw = fi.CreateText(); //檔案不存在 產生檔案 
        }
        
        sw.WriteLine("Create log file");
        sw.WriteLine(DateTime.Now.ToString("u") + " " + program_name1 + " Program Start");
        sw.WriteLine(DateTime.Now.ToString("u") + " " + program_name1 + " Program END ");
        sw.WriteLine("");
        sw.Close();


    }

    public static void delete_log_dir(string file_path,string file_type, Double dayAgo)
    {
        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(".") + "\\CF\\Save_file\\"); 
        DirectoryInfo dir = new DirectoryInfo(file_path);
        // FileInfo[] files = dir.GetFiles("*.xls"); 
        //FileInfo[] files = dir.GetFiles(file_type);

         DirectoryInfo[] subdir = dir.GetDirectories();


         for (int i = 0; i <= subdir.Length - 1; i++)
        {
            if (subdir[i].CreationTime < DateTime.Now.AddDays(dayAgo))
            {
                FileInfo[] files = subdir[i].GetFiles(file_type);

                for (int j = 0; j <= files.Length-1; j++)
                {
                    files[j].Delete();

                }
                subdir[i].Delete();
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


    public static void delete_log_file(string file_path, string file_type, Double dayAgo)
    {
        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(".") + "\\CF\\Save_file\\"); 
        DirectoryInfo dir = new DirectoryInfo(file_path);
        // FileInfo[] files = dir.GetFiles("*.xls"); 
        FileInfo[] files = dir.GetFiles(file_type);


        for (int i = 0; i <= files.Length - 1; i++)
        {
            if (files[i].CreationTime < DateTime.Now.AddDays(dayAgo))
            {

                files[i].Delete();
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



    public static  string get_ticket(string location,string user_id)
    {
        ArrayList al_temp = new ArrayList();
        al_temp = func.FileToArray(location);
        
        Int32 initial = 0;

        string str_ticket = "N";

        for (int i = 0; i < al_temp.Count; i++)
        {
            if (al_temp[i].ToString() == user_id)
            {
                initial++;
            }
        }


        if (initial > 0)
        {
            str_ticket = "Y";

        }
        return str_ticket;

    }

    public static string GetWeekOfCurrDate(DateTime dt)
    {
        int Week = 1;
        int nYear = dt.Year;
        System.DateTime FirstDayInYear = new DateTime(nYear, 1, 1);
        System.DateTime LastDayInYear = new DateTime(nYear, 12, 31);
        int DaysOfYear = Convert.ToInt32(LastDayInYear.DayOfYear);
        int WeekNow = Convert.ToInt32(FirstDayInYear.DayOfWeek) - 1;
        if (WeekNow < 0) WeekNow = 6;
        int DayAdd = 6 - WeekNow;
        System.DateTime BeginDayOfWeek = new DateTime(nYear, 1, 1);
        System.DateTime EndDayOfWeek = BeginDayOfWeek.AddDays(DayAdd);
        Week = 2;
        for (int i = DayAdd + 1; i <= DaysOfYear; i++)
        {
            BeginDayOfWeek = FirstDayInYear.AddDays(i);
            if (i + 6 > DaysOfYear)
            {
                EndDayOfWeek = BeginDayOfWeek.AddDays(DaysOfYear - i - 1);
            }
            else
            {
                EndDayOfWeek = BeginDayOfWeek.AddDays(6);
            }

            if (dt.Month == EndDayOfWeek.Month && dt.Day <= EndDayOfWeek.Day)
            {
                break;
            }
            Week++;
            i = i + 6;
        }
        string date_year_week = dt.Year + Week.ToString();
        return date_year_week;
    }


    public static string MergeMailListToString(string sql, string conn)
    {
        DataSet maillistX = new DataSet();

        string connx = System.Configuration.ConfigurationManager.AppSettings["OARPT"];

        connx = conn;
        string sql_mailist = "select * from dmbs_ce_alarm_mail_cfg t                                 " +
                             "where t.material_group='Chemical' and t.material='DMSO' and plan='T1'  ";


        sql_mailist = sql;

        //ArrayList maillist = new ArrayList();

        // maillist = func.FileToArray(Server.MapPath(".") + "\\maillist.txt"); 
        maillistX = func.get_dataSet_access(sql_mailist, connx);

        string maillist1 = "";
        string maillist2 = "";

        for (int i = 0; i <= maillistX.Tables[0].Rows.Count - 1; i++)
        {

            if (maillistX.Tables[0].Rows.Count == 1)
            {
                maillist1 = maillistX.Tables[0].Rows[i]["MAIL_ADDRESS"].ToString();
            }
            else

                maillist1 = maillistX.Tables[0].Rows[i]["MAIL_ADDRESS"].ToString() + ","; //呈現每一個 DataSet Row[i] 
            maillist2 = maillist2 + maillist1; //將每個 DataSet Row[i] 的值串起來 
        }
        return maillist2;
    }

    public static void get_sql_execute(string sql_str, string strConn)
    {
        string strConn2;
        strConn2 = strConn;

        //strConn2 = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\\stock.mdb; Persist Security Info=True;"; 
        string sql_str2;

        sql_str2 = sql_str;



        OleDbConnection myConnection = new OleDbConnection(strConn2);

        OleDbCommand myCommand = new OleDbCommand(sql_str2, myConnection);
        
        myConnection.Open();
        myCommand.CommandTimeout = 900;


        //OleDbDataReader MyReader = myCommand.ExecuteReader();
        try
        {
             OleDbDataReader MyReader = myCommand.ExecuteReader();
             MyReader.Read();

             MyReader.Close();

             myConnection.Close();

        }
        catch (Exception)
        {
            myConnection.Close();
            throw;
        }

       


    }

    public static DataSet get_dataSet_access_oracle_client(string strSql, string conn)
    {

        //"Password=pmeda;User ID=lcdapp;Data Source=peda1;Persist Security Info=True"
        //OracleCommand myCommand = new OracleCommand(strSql, myConnection);
        //OracleDataAdapter myAdapter = new OracleDataAdapter();
        //myAdapter.SelectCommand = myCommand;
        //DataSet ds = new DataSet();
        //myAdapter.Fill(ds);
        ////myCommand.Dispose();
        ////myAdapter.Dispose();
        //return ds;

         OracleConnection orcn = new OracleConnection(conn);


         OracleCommand cmd = new OracleCommand(strSql, orcn);


         OracleDataAdapter oda2 = new OracleDataAdapter(cmd);

        oda2.SelectCommand.CommandTimeout = 900;
        DataSet ds = new DataSet();
        oda2.Fill(ds, "test");
        return ds;

    }

    public static DataSet get_dataSet_access(string sql_str, string strConn)
    {
        string strConn2;
        strConn2 = strConn;

        //strConn2 = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\\stock.mdb; Persist Security Info=True;"; 
        string sql_str2;

        sql_str2 = sql_str;

        OleDbDataAdapter oda2 = new OleDbDataAdapter(sql_str2, strConn2);

        oda2.SelectCommand.CommandTimeout = 900;

        DataSet myDataSet2 = new DataSet();
        oda2.Fill(myDataSet2, "AccessInfo");
        return myDataSet2;

    }



    private static DataTable get_dataTable(string sql_str, string strConn)
    {
        string strConn2;
        strConn2 = strConn;

        //strConn2 = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\\stock.mdb; Persist Security Info=True;"; 
        string sql_str2;

        sql_str2 = sql_str;

        OleDbDataAdapter oda2 = new OleDbDataAdapter(sql_str2, strConn2);

        DataTable myDataTable2 = new DataTable();
        oda2.Fill(myDataTable2, "AccessInfo");
        return myDataTable2;

    }



    public static DataTable Table_transport(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("ColumnName", typeof(string));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtNew.Columns.Add("Column" + (i + 1).ToString(), typeof(string));
        }
        foreach (DataColumn dc in dt.Columns)
        {
            DataRow drNew = dtNew.NewRow();
            drNew["ColumnName"] = dc.ColumnName;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                drNew[i + 1] = dt.Rows[i][dc].ToString();
            }
            dtNew.Rows.Add(drNew);
        }

        return dtNew;
    }

    public static DataTable Table_transport1(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("ColumnName", typeof(string));

        //build New DataTable
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtNew.Columns.Add("Column" + (i + 1).ToString(), typeof(string));
            //dtNew.Columns.Add("CycleTime", typeof(string));
        }
        // x y  transport filldata
        for (int j = 0; j < dt.Columns.Count; j++)
        {
            DataRow drNew = dtNew.NewRow();



            for (int i = 0; i < dt.Rows.Count; i++)
            {

                drNew["ColumnName"] = dt.Columns[j].ColumnName;

                drNew[i + 1] = dt.Rows[i][j].ToString();


            }

            dtNew.Rows.Add(drNew);

        }




        return dtNew;
    }

    public static ArrayList FileToArray(string srtPath)
    {
        StreamReader ReadFile = new StreamReader(srtPath, // 檔案路徑
    System.Text.Encoding.Default); // 編碼方式

        ArrayList arrFile = new ArrayList(); //make our temporary storage object
        string strTmp;

        //loop through all the rows, stopping when we reach the end of file
        do
        {
            strTmp = ReadFile.ReadLine();
            if (strTmp != null)
            {
                strTmp = strTmp.Trim();
                if (strTmp.Length != 0) arrFile.Add(strTmp); //add each element to our ArrayList
            }
        } while (strTmp != null);
        ReadFile.Close();
        return arrFile;
    } 


    //public static void BindGridView(string sql, string strConn)
    //{
    //    string sql_str, strConn2;
    //    DataSet myDataSet123 = new DataSet();
    //    sql_str = sql;
    //    //sql_str = "select * from stock_macd_1 WHERE DATE>DATEADD('d',-1,NOW()) order by DATE desc"; 
    //    //GridView GridViewn = new GridView(); 

    //    //DATEADD(day, 1, @CountDate) 
    //    strConn2 = strConn;

    //    myDataSet123 = get_dataSet_access(sql_str, strConn2);

    //    GridView1.DataSource = myDataSet123.Tables["AccessInfo"].DefaultView;
    //    GridView1.DataBind();
    //} 



    public static string ArrayListToString(string file_path)
    {

        ArrayList maillist = new ArrayList();

        // maillist = func.FileToArray(Server.MapPath(".") + "\\maillist.txt"); 
        maillist = func.FileToArray(file_path);

        string maillist1 = "";
        string maillist2 = "";

        for (int i = 0; i <= maillist.Count - 1; i++)
        {

            if (i == maillist.Count - 1)
            {
                maillist1 = maillist[i].ToString();
            }
            else

                maillist1 = maillist[i] + ","; //呈現每一個 ArrayList[i] 
            maillist2 = maillist2 + maillist1; //將每個ArrayList[i]的值串起來 
        }
        return maillist2;
    }


    public static void Upload(string filename, string ftpServerIP, string Account, string ftppassword)
    {

        FileInfo fileInf = new FileInfo(filename);

        string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;

        FtpWebRequest reqFTP;

        // Create FtpWebRequest object from the Uri provided

        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileInf.Name));//此段關鍵

        // Provide the WebPermission Credintials

        reqFTP.Credentials = new NetworkCredential(Account, ftppassword);

        // By default KeepAlive is true, where the control connection is not closed

        // after a command is executed.

        reqFTP.KeepAlive = false;

        // Specify the command to be executed.

        reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

        // Specify the data transfer type.

        reqFTP.UseBinary = true;

        // Notify the server about the size of the uploaded file

        reqFTP.ContentLength = fileInf.Length;

        // The buffer size is set to 2kb

        int buffLength = 2048;

        byte[] buff = new byte[buffLength];

        int contentLen;

        // Opens a file stream (System.IO.FileStream) to read the file to be uploaded

        FileStream fs = fileInf.OpenRead();

        try
        {

            // Stream to which the file to be upload is written

            Stream strm = reqFTP.GetRequestStream();

            // Read from the file stream 2kb at a time

            contentLen = fs.Read(buff, 0, buffLength);

            // Till Stream content ends

            while (contentLen != 0)
            {

                // Write Content from the file stream to the FTP Upload Stream

                strm.Write(buff, 0, contentLen);

                contentLen = fs.Read(buff, 0, buffLength);

            }

            // Close the file stream and the Request Stream

            strm.Close();

            fs.Close();

        }


        catch (Exception ex)
        {

            //MessageBox.Show(ex.Message, "Upload Error");

        }

    }



}
