using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.IO;
using System.Web.Mail;

/// <summary>
/// dbutil 的摘要描述
/// </summary>
///
public class dbutil
{
    public dbutil()
    {

    }

    public string getConnection()
    {
        return System.Configuration.ConfigurationSettings.AppSettings["dsn"];
    }

    public DataSet GetDataset(string strSql)
    {
        OleDbConnection myConnection = new OleDbConnection(getConnection());
        OleDbDataAdapter myCommand = new OleDbDataAdapter(strSql, myConnection);
        DataSet ds = new DataSet();
        myCommand.Fill(ds);
        myConnection.Close();
        myConnection.Dispose();
        myCommand.Dispose();
        return ds;

    }

    public DataSet GetDataset(string strSql, OleDbConnection myConnection)
    {
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        myAdapter.SelectCommand = myCommand;
        DataSet ds = new DataSet();
        myAdapter.Fill(ds);
        myCommand.Dispose();
        myAdapter.Dispose();
        return ds;
    }

    public DataSet GetDataset(string strSql, OracleConnection myConnection)
    {
        OracleCommand myCommand = new OracleCommand(strSql, myConnection);
        OracleDataAdapter myAdapter = new OracleDataAdapter();
        myAdapter.SelectCommand = myCommand;
        DataSet ds = new DataSet();
        myAdapter.Fill(ds);
        //myCommand.Dispose();
        //myAdapter.Dispose();
        return ds;
    }

    public OleDbDataReader GetDatareader(string strSql)
    {
        OleDbConnection myConnection = new OleDbConnection(ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"]);
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        myConnection.Open();
        OleDbDataReader MyReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        return MyReader;
    }

    public OleDbDataReader GetDatareader(string strSql, OleDbConnection myConnection)
    {
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        OleDbDataReader MyReader = myCommand.ExecuteReader();
        return MyReader;
    }

    public OracleDataReader GetDatareader(string strSql, OracleConnection myConnection)
    {
        OracleCommand myCommand = new OracleCommand(strSql, myConnection);
        OracleDataReader MyReader = myCommand.ExecuteReader();
        return MyReader;
    }

    public OleDbDataReader GetEmpDatareader(string strSql)
    {
        OleDbConnection myConnection = new OleDbConnection(ConfigurationSettings.AppSettings["sso"]);
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        myConnection.Open();
        OleDbDataReader MyReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        return MyReader;
    }

    public OleDbDataReader GetEmpDatareader(string strSql, OleDbConnection myConnection)
    {
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        OleDbDataReader MyReader = myCommand.ExecuteReader();
        return MyReader;
    }

    public void ExecuteStatement(string strSql)
    {
        OleDbConnection myConnection = new OleDbConnection(ConfigurationSettings.AppSettings["DBCONN_OARPT_OLE"]);
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        myCommand.Connection.Open();
        myCommand.ExecuteNonQuery();

        myConnection.Close();
        myConnection.Dispose();
        myCommand.Dispose();
    }

    public void ExecuteStatement(string strSql, OleDbConnection myConnection)
    {
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        myCommand.ExecuteNonQuery();
        myCommand.Dispose();
    }

    public void ExecuteStatement(string strSql, OracleConnection myConnection)
    {
        OracleCommand myCommand = new OracleCommand(strSql, myConnection);
        myCommand.ExecuteNonQuery();
        myCommand.Dispose();
    }


    public void LoginHistory(string ip, string user_id, string func_name, string system)
    {

        string user_name = "Guest";
        string user_dept = "Guest";
        if (HttpContext.Current.Session["user_id"] != null)
        {
            user_name = HttpContext.Current.Session["user_id"].ToString();
        }
        if (HttpContext.Current.Session["user_dept"] != null)
        {
            user_dept = HttpContext.Current.Session["user_dept"].ToString();
        }

        OleDbConnection myConnection = new OleDbConnection(ConfigurationSettings.AppSettings["dsn"]);
        string strSql = "insert into login_history (system_id,function_id,user_ip,user_id,user_name,user_dept_id,login_dttm) values ('" + system + "','" + func_name + "','" + ip + "','" + user_id.ToUpper() + "','" + user_name + "','" + user_dept + "',sysdate)";

        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        myCommand.Connection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
        myConnection.Dispose();
    }

    public void setUserData()
    {
        string strSql = "";
        DataSet ds = new DataSet();

        if (HttpContext.Current.Session["user_name"] == null)
        {
            strSql = "select cname,dept from tms_empinfo t where t.empno='" + HttpContext.Current.User.Identity.Name.Substring(8).ToUpper() + "'";
            ds = GetDataset(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpContext.Current.Session["user_name"] = ds.Tables[0].Rows[0]["cname"].ToString();
                HttpContext.Current.Session["user_dept"] = ds.Tables[0].Rows[0]["dept"].ToString();
            }
        }
    }

//    public void saveFile(HtmlInputFile FU, string FD, string Task_ID, string AI_ID)
//    {
//        string AttachDir = System.Configuration.ConfigurationSettings.AppSettings["AttachDir"];
//        string AttachVirtualDir = System.Configuration.ConfigurationSettings.AppSettings["AttachVirtualDir"];
//        string path = "";
//        string url = "";
//        string fn = System.IO.Path.GetFileName(FU.PostedFile.FileName);
//        DirectoryInfo di;
//        string strSql = "";

//        if (AI_ID == "")
//        {
//            di = new DirectoryInfo(AttachDir + "\\" + Task_ID + "\\");
//            path = AttachDir + "\\" + Task_ID + "\\";
//            url = "http://" + HttpContext.Current.Server.MachineName + "/" + AttachVirtualDir + "/" + Task_ID + "/" + fn;
//            strSql = @" insert into tms_attachment (task_id, file_desc, file_link) 
//                    values ('{0}',?,?)";
//            strSql = string.Format(strSql, Task_ID, url);
//        }
//        else
//        {
//            di = new DirectoryInfo(AttachDir + "\\" + Task_ID + "\\" + AI_ID + "\\");
//            path = AttachDir + "\\" + Task_ID + "\\" + AI_ID + "\\";
//            url = "http://" + HttpContext.Current.Server.MachineName + "/" + AttachVirtualDir + "/" + Task_ID + "/" + AI_ID + "/" + fn;
//            strSql = @" insert into tms_attachment (task_id,ai_id, file_desc, file_link) 
//                    values ('{0}','{1}',?,?)";
//            strSql = string.Format(strSql, Task_ID, AI_ID, url);
//        }

//        if (!di.Exists)
//            di.Create();

//        FU.PostedFile.SaveAs(path + fn);

//        OleDbConnection myConnection = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["dsn"]);
//        myConnection.Open();
//        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
//        myCommand.Parameters.Add(new OleDbParameter("@file_desc", OleDbType.VarChar, 200));
//        myCommand.Parameters["@file_desc"].Value = FD;

//        myCommand.Parameters.Add(new OleDbParameter("@file_link", OleDbType.VarChar, 500));
//        myCommand.Parameters["@file_link"].Value = url;

//        myCommand.ExecuteNonQuery();
//        myConnection.Close();
//    }

    //Bunny's saveFile
    public void mysaveFile(HtmlInputFile FU, string FD, string Task_ID, string AI_ID)
    {
        string AttachDir = System.Configuration.ConfigurationSettings.AppSettings["AttachDir"];
        string AttachVirtualDir = System.Configuration.ConfigurationSettings.AppSettings["AttachVirtualDir"];
        string path = "";
        string url = "";
        string fn = System.IO.Path.GetFileName(FU.PostedFile.FileName);
        DirectoryInfo di;
        string strSql = "";
        string strYear = DateTime.UtcNow.AddHours(8).ToString("yyyy"); 
        
        di = new DirectoryInfo(AttachDir + "\\" + strYear + "\\");
        path = AttachDir + "\\" + strYear + "\\";
        url = "http://" + HttpContext.Current.Server.MachineName + "/" + AttachVirtualDir + "/" + strYear + "/" + fn;
        strSql = @" insert into tms_attachment (task_id, file_desc, file_link) 
                values ('{0}',?,?)";
        strSql = string.Format(strSql, Task_ID, url);        

        if (!di.Exists)
            di.Create();

        FU.PostedFile.SaveAs(path + fn);

        OleDbConnection myConnection = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["dsn"]);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        myCommand.Parameters.Add(new OleDbParameter("@file_desc", OleDbType.VarChar, 200));
        myCommand.Parameters["@file_desc"].Value = FD;

        myCommand.Parameters.Add(new OleDbParameter("@file_link", OleDbType.VarChar, 500));
        myCommand.Parameters["@file_link"].Value = url;

        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }

    public void saveFile(HtmlInputFile FU, string FD, string Task_ID, string AI_ID)
    {
        string AttachDir = System.Configuration.ConfigurationSettings.AppSettings["AttachDir"];
        string AttachVirtualDir = System.Configuration.ConfigurationSettings.AppSettings["AttachVirtualDir"];
        string path = "";
        string url = "";
        string fn = System.IO.Path.GetFileName(FU.PostedFile.FileName);
        DirectoryInfo di;
        string strSql = "";
        if (AI_ID == "")
        {
            di = new DirectoryInfo(AttachDir + "\\task" + "\\" + Task_ID + "\\");
            path = AttachDir + "\\task" + "\\" + Task_ID + "\\";
            url = "http://" + HttpContext.Current.Server.MachineName + "/" + AttachVirtualDir + "/task" + "/" + Task_ID + "/" + fn;
            strSql = @" insert into tms_attachment (task_id, file_desc, file_link) 
                    values ('{0}',?,?)";
            strSql = string.Format(strSql, Task_ID, url);
        }
        else
        {
            di = new DirectoryInfo(AttachDir + "\\task" + "\\" + Task_ID + "\\" + AI_ID + "\\");
            path = AttachDir + "\\task" + "\\" + Task_ID + "\\" + AI_ID + "\\";
            url = "http://" + HttpContext.Current.Server.MachineName + "/" + AttachVirtualDir + "/task" + "/" + Task_ID + "/" + AI_ID + "/" + fn;
            strSql = @" insert into tms_attachment (task_id,ai_id, file_desc, file_link) 
                    values ('{0}','{1}',?,?)";
            strSql = string.Format(strSql, Task_ID, AI_ID, url);
        }

        if (!di.Exists)
            di.Create();

        FU.PostedFile.SaveAs(path + fn);

        OleDbConnection myConnection = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["dsn"]);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        myCommand.Parameters.Add(new OleDbParameter("@file_desc", OleDbType.VarChar, 200));
        myCommand.Parameters["@file_desc"].Value = FD;

        myCommand.Parameters.Add(new OleDbParameter("@file_link", OleDbType.VarChar, 500));
        myCommand.Parameters["@file_link"].Value = url;

        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }

    public void saveFile(HtmlInputFile FU, string FD, string Project_ID)
    {
        string AttachDir = System.Configuration.ConfigurationSettings.AppSettings["AttachDir"];
        string AttachVirtualDir = System.Configuration.ConfigurationSettings.AppSettings["AttachVirtualDir"];
        string path = "";
        string url = "";
        string fn = System.IO.Path.GetFileName(FU.PostedFile.FileName);
        DirectoryInfo di;
        string strSql = "";

        //if (Project_ID != "")
        //{
        di = new DirectoryInfo(AttachDir + "\\project" + "\\" + Project_ID + "\\");
        path = AttachDir + "\\project" + "\\" + Project_ID + "\\";
        url = "http://" + HttpContext.Current.Server.MachineName + "/" + AttachVirtualDir + "/project" + "/" + Project_ID + "/" + fn;
        strSql = @" insert into tms_attachment (project_id, file_desc, file_link) 
                    values ('{0}',?,?)";
        strSql = string.Format(strSql, Project_ID, url);
        //}

        if (!di.Exists)
            di.Create();

        FU.PostedFile.SaveAs(path + fn);

        OleDbConnection myConnection = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["dsn"]);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand(strSql, myConnection);
        myCommand.Parameters.Add(new OleDbParameter("@file_desc", OleDbType.VarChar, 200));
        myCommand.Parameters["@file_desc"].Value = FD;

        myCommand.Parameters.Add(new OleDbParameter("@file_link", OleDbType.VarChar, 500));
        myCommand.Parameters["@file_link"].Value = url;

        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }

    public void sendMail(string Mail_Body, string Mail_To, string Mail_Subject)
    {
        MailMessage objMailMessage = new MailMessage();
        SmtpMail objmail;

        objMailMessage.BodyFormat = MailFormat.Html;
        objMailMessage.Body = Mail_Body;
        objMailMessage.To = Mail_To;
        objMailMessage.From = "CIM_Innovation@innolux.com.tw";
        objMailMessage.Subject = Mail_Subject;

        try
        {
            SmtpMail.SmtpServer = "10.56.130.63";
            SmtpMail.Send(objMailMessage);
        }
        catch
        {
            try
            {
                SmtpMail.SmtpServer = "10.56.130.57";
                SmtpMail.Send(objMailMessage);
            }
            catch
            {
            }
        }
    }
}
