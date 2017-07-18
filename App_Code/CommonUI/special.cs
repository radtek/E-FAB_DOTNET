using System;
using System.Web.Mail;
using System.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using System.Collections;
using Innolux.Portal.EntLibBlock.DataAccess;
namespace Innolux.Portal.CommonFunction
{
	/// <summary>
	/// special 的摘要描述。
	/// </summary>
	public class special
	{

        DbAccessHelper m_objDB = new DbAccessHelper();
        
		public special()
		{
			//
			// TODO: 在此加入建構函式的程式碼
			//
		}
		/// <summary>
		/// Attachment_flag=1 true else false
		/// if Attachment_flag=1 then file_path=absolutely path   else absolutely path=null
		/// </summary>
		/// <param name="mail_body"></param>
		/// <param name="mail_from"></param> 
		/// <param name="mail_to"></param>
		/// <param name="mail_subject"></param>
		/// <param name="Attachment_flag">Attachment_flag=1 true else false</param>
		/// <param name="file_path">absolutely path</param>
		public void Send_mail(string mail_body,string mail_from,string mail_to,string mail_subject,int Attachment_flag,string file_path)
		{
			MailMessage Message = new MailMessage();
			Message.BodyEncoding=Encoding.UTF8;
			Message.To=mail_to;
			if(Attachment_flag==1)
			{
				MailAttachment ma=new MailAttachment(file_path);
			
				Message.Attachments.Add(ma);
			}
			else
			{
				Message.Attachments.Clear();
			
			}
			
			Message.From=mail_from;
			Message.Subject =mail_subject;			
			Message.BodyFormat = MailFormat.Html;			
			Message.Body = mail_body;
			SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["MAIL_SERVER"];
			try
			{
				SmtpMail.Send(Message);
			}
			catch
			{
			SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["MAIL_SERVER_bak"];
			SmtpMail.Send(Message);
			}
		}
		/// <summary>
		/// Attachment_flag=1 true else false
		/// if Attachment_flag=1 then file_path=absolutely path   else absolutely path=null
		/// </summary>
		/// <param name="mail_body"></param>
		/// <param name="mail_from"></param> 
		/// <param name="mail_to"></param>
		/// <param name="mail_cc"></param>
		/// <param name="mail_subject"></param>
		/// <param name="Attachment_flag">Attachment_flag=1 true else false</param>
		/// <param name="file_path">absolutely path</param>

		public void Send_mail(string mail_body,string mail_from,string mail_to,string mail_cc,string mail_subject,int Attachment_flag,string file_path)
		{
			MailMessage Message = new MailMessage();
			Message.BodyEncoding=Encoding.UTF8;
			Message.To=mail_to;
			Message.Cc=mail_cc;
            
			if(Attachment_flag==1)
			{
				MailAttachment ma=new MailAttachment(file_path);
			
				Message.Attachments.Add(ma);
			}
			else
			{
				Message.Attachments.Clear();
			
			}
			Message.From=mail_from;
			Message.Subject =mail_subject;
			
			Message.BodyFormat = MailFormat.Html;			
			Message.Body = mail_body;
			try
			{
				SmtpMail.Send(Message);
			}
			catch
			{
				SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["MAIL_SERVER_bak"];
				SmtpMail.Send(Message);
			}
		
		
		
		}
		/// <summary>
		/// S string  s_delimStr:delimStr: "," or ";" or "_" or .......
		/// </summary>
		/// <param name="s"></param>
		/// <param name="s_delimStr"></param>
		/// <returns></returns>
		public ArrayList Get_al(string s,string s_delimStr)
		{
		ArrayList al=new ArrayList();
		string delimStr = s_delimStr;
		char [] delimiter = delimStr.ToCharArray();
		string [] s1=s.Split(delimiter);
			foreach(string s2 in s1)
			{
			al.Add(s2);
			
			}
		return al;

		}
		public void PeruseHistory(string ip,string user_id,string func_name,string project_id) 
		{
			string user_name="Guest";
			string strSql="";
            ITransactionManager trans = m_objDB.TracationManager;
            try
            {
                trans.BeginTransaction();
                if (HttpContext.Current.Session["user_name"] != null)
                {
                    user_name = HttpContext.Current.Session["user_name"].ToString();
                    strSql = "insert into peruse_history values ('" + ip + "','" + user_id.ToUpper() + "','" + user_name + "','" + func_name + "',sysdate,'" + project_id + "')";
                }
                else
                {
                    strSql = "insert into peruse_history values ('" + ip + "','" + user_id.ToUpper() + "', (select cname from ssouser.fwempinfo t where t.empno='" + user_id.ToUpper() + "'),'" + func_name + "',sysdate,'" + project_id + "')";
                }
                m_objDB.ExecutionSql(strSql);
                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
            }
		}



		public void Thread_Sleep(int timeout)
		{
			System.Threading.Thread.Sleep(timeout);

		
		}
		public void Zip_File(string root_path,string zipfile_name,string file_path,string file_name)
		{
		
		System.Diagnostics.Process.Start(root_path+@"\winrar.exe", " a -ep " + root_path+@"\zip\"+zipfile_name + " " + file_path+@"\" + file_name) ;
		
		}
		public void Load_profile(DataSet ds,ArrayList al)
		{
		Object o;
			for(int i=0;i<al.Count;i++)
			{
				o=al[i];
				switch(o.ToString())
				{
					case "System.Web.UI.WebControls.DropDownList":
						DropDownList dl;				
						dl=(DropDownList)o;
						if(dl.ClientID.ToLower().StartsWith("s_"))
						{
							for(int j=0;j<ds.Tables[0].Columns.Count;j++)
					 		{
								if(ds.Tables[0].Columns[j].Caption.Equals(dl.ID))
								{
									
									try
									{
										dl.SelectedValue=ds.Tables[0].Rows[0][j].ToString();
									}
									catch
									{
										dl.SelectedIndex=0;
									}
									
								}
					 		}
					    }

						break;

					default:
				
						break;
				
				
				
				
				}
			}
		
		}
		public void Ftp_upload(string setRemoteHost,string setRemoteUser,string setRemotePass,string dir,string file_path)
		{
		FTPFactory ff=new FTPFactory();
		ff.setRemoteHost(setRemoteHost);
		ff.setRemoteUser(setRemoteUser);
		ff.setRemotePass(setRemotePass);
		ff.login();
		ff.chdir(dir);
		ff.setBinaryMode(true);
		ff.upload(file_path);
		ff.close();
		}
		public  string gethtml(DataTable ds)
		{

			StringBuilder sb=new StringBuilder();
			sb.Append("<H1>DAILY_CHK</H1>");
            sb.Append("<table style='font-family:Arial;font-size: 14px;' bordercolor =Black cellpadding='1' cellspacing='1'  width='100%'>");
            sb.Append("<tr bgcolor=#003366 style='color:White'><td>&nbsp;&nbsp;</td>");
			foreach(DataColumn myCol in ds.Columns)
			{
				sb.Append("<td >&nbsp;"+myCol.Caption+"&nbsp;</td>");

			}
			for(int i=0;i<ds.Rows.Count;i++)
			{

				sb.Append("<tr>");
				for(int j=0;j<ds.Columns.Count;j++)
				{
					if(j==0)
					{
						sb.Append("<td >&nbsp;&nbsp;</td>");
					}
					sb.Append("<td >&nbsp;"+ds.Rows[i][j].ToString()+"&nbsp;</td>");
			}
				sb.Append("</tr>");
				
			}
			sb.Append("</table>");
			return sb.ToString();
		}



	}
}
