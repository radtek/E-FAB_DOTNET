using System;
using System.Web.Mail;
using System.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using System.Collections;
using Innolux.Portal.CommonFunction;
namespace IS.util
{
	/// <summary>
	/// special 的摘要描述。
	/// </summary>
	public class special
	{
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
			if(HttpContext.Current.Session["user_name"] != null)
			{
				user_name=HttpContext.Current.Session["user_name"].ToString();
				strSql="insert into peruse_history values ('" + ip + "','" + user_id.ToUpper() + "','" + user_name + "','" + func_name + "',sysdate,'"+project_id+"')";
			}		
			else
			{
				strSql="insert into peruse_history values ('" + ip + "','" + user_id.ToUpper() + "', (select cname from ssouser.fwempinfo t where t.empno='" + user_id.ToUpper() +"'),'" + func_name + "',sysdate,'"+project_id+"')";
			}
			db.ExecuteStatement(strSql,5);
		
		
		
		
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

			// D5D291 ==> 8080FF =>C0CBFE => DDE3FF
			StringBuilder sb=new StringBuilder();
			sb.Append("<table border=1 background-color=#DDE3FF;border-bottom= 1px solid #DDE3FF; border-right=1px solid #DDE3FF;border-left=1px solid #DDE3FF;border-top=1px solid #ffffff;   width=80%   cellspacing=1 cellpadding=1  >");
			sb.Append("<tr>") ;
			foreach(DataColumn myCol in ds.Columns)
			{
				sb.Append("<td bgcolor=#DDE3FF font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;>&nbsp;"+myCol.Caption+"&nbsp;</td>");

			}
			sb.Append("</tr>") ;
			for(int i=0;i<ds.Rows.Count;i++)
			{

				sb.Append("<tr>");
				for(int j=0;j<ds.Columns.Count;j++)
				{
					if ( j == 0 )
						sb.Append("<td bgcolor=#DDE3FF font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;>&nbsp;"+ds.Rows[i][j].ToString()+"&nbsp;</td>");
					else
						sb.Append("<td bgcolor=#DDE3FF font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"&nbsp;</b></td>");
				}
				sb.Append("</tr>");
				
			}
			sb.Append("</table>");
			return sb.ToString();

		}
		/*
		// for T0/T1 生產快遞
		//新增顏色配置及數字小於0會以紅色顯示(T1CF及Yield良率不判斷)
		//新增TP資料及TP良率合併呈現
		//2009-05-25
		public  string gethtml_2(DataTable ds)
		{

			// D5D291 ==> 8080FF =>C0CBFE => DDE3FF
			StringBuilder sb=new StringBuilder();
			//sb.Append("<table border=1 background-color=#D5D291;border-bottom= 1px solid #DDE3FF; border-right=1px solid #DDE3FF;border-left=1px solid #DDE3FF;border-top=1px solid #ffffff;   width=80%   cellspacing=1 cellpadding=1  >");
			sb.Append("<table border=1 width=80% cellspacing=1 cellpadding=1>");
			sb.Append("<tr>") ;
			foreach(DataColumn myCol in ds.Columns)
			{
				sb.Append("<td bgcolor=#FFA953 font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;>&nbsp;" + "<b>" + myCol.Caption + "</b>" + "&nbsp;</td>");
			}
			sb.Append("</tr>") ;
			for(int i=0;i<ds.Rows.Count;i++)
			{

				sb.Append("<tr>");
				for(int j=0;j<ds.Columns.Count;j++)
				{
					if ( j == 0 )
					{
						sb.Append("<td bgcolor=#FFFFA6 font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;>&nbsp;" + ds.Rows[i][j].ToString()+"&nbsp;</td>");
					}					
					else
					{
						//TP Yield用合併欄位的方式呈現
						if(i==9 && j==3)
						{
							sb.Append("<td rowspan='2' font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;" + ds.Rows[i][j].ToString()  + "&nbsp;</b></td>");
						}
						//因為合併TP yield的關係，所以最後一格就不印出來了
						else if(i==10 && j==3)
						{
							break;
						}
						//for T1CF & Yield欄位，不判斷小於0給紅色
						else if( (i==4 && j==1) || j==3 )
						{
							sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"&nbsp;</b></td>");
						}						
						else
						{							
							if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
							{													
								sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "</font>" + "&nbsp;</b></td>");								
							}
							else
							{																							
								sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"&nbsp;</b></td>");															
							}						
						}						
					}
					
				}
				sb.Append("</tr>");
				
			}
			sb.Append("</table>");			
			return sb.ToString();
		}// end of gethtml_2
		//*/
		
		// for T0/T1 生產快遞
		//新增顏色配置及數字小於0會以紅色顯示(T1CF及Yield良率不判斷)
		//新增TP資料及TP良率合併呈現
		//新增尺寸單位
		//2009-06-02
		public  string gethtml_2(DataTable ds)
		{

			// D5D291 ==> 8080FF =>C0CBFE => DDE3FF
			StringBuilder sb=new StringBuilder();
			//sb.Append("<table border=1 background-color=#D5D291;border-bottom= 1px solid #DDE3FF; border-right=1px solid #DDE3FF;border-left=1px solid #DDE3FF;border-top=1px solid #ffffff;   width=80%   cellspacing=1 cellpadding=1  >");
			sb.Append("<table border=1 width=80% cellspacing=1 cellpadding=1>");
			sb.Append("<tr>") ;
			foreach(DataColumn myCol in ds.Columns)
			{
				sb.Append("<td bgcolor=#FFA953 font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;>&nbsp;" + "<b>" + myCol.Caption + "</b>" + "&nbsp;</td>");
			}
			sb.Append("</tr>") ;
			for(int i=0;i<ds.Rows.Count;i++)
			{

				sb.Append("<tr>");
				for(int j=0;j<ds.Columns.Count;j++)
				{
					if ( j == 0 )
					{
						sb.Append("<td bgcolor=#FFFFA6 font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;>&nbsp;" + ds.Rows[i][j].ToString()+"&nbsp;</td>");
					}					
					else
					{
						//TP Yield用合併欄位的方式呈現
						if(i==9 && j==3)
						{
							sb.Append("<td rowspan='2' font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;" + ds.Rows[i][j].ToString()  + "&nbsp;</b></td>");
						}
							//因為合併TP yield的關係，所以最後一格就不印出來了
						else if(i==10 && j==3)
						{
							break;
						}
							//for T1CF & Yield欄位，不判斷小於0給紅色
							//else if( (i==4 && j==1) || j==3 )
						else if( i==4 && j==1)
						{
							sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+" (Sheet)&nbsp;</b></td>");
						}		
						else if( j== 3 )
						{
							sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"&nbsp;</b></td>");
						}
						else
						{
							//T0Array,T1Array Output & Input
							if( i==0 && j==1 || i==0 && j == 2 || i==1 && j==1 || i==1 && j==2 )
							{
								
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Sheet)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Sheet)&nbsp;</b></td>");															
								}						
							}
							//T0Cell & T1Cell Input Sheet
							if( i==2 && j==1 || i==3 && j==1 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Sheet)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Sheet)&nbsp;</b></td>");															
								}	
							}
							//T0Cell Output Cut
							if( i==2 && j==2 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Cut)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Cut)&nbsp;</b></td>");															
								}	
							}

							//T1Cell Output Chip
							if( i==3 && j==2 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Chip)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Chip)&nbsp;</b></td>");															
								}	
							}


							//T1CF Input/Output Sheet
							if( i==4 && j==2 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Sheet)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Sheet)&nbsp;</b></td>");															
								}	
							}

							//LH FEOL2 Input/Output Cut
							if( i==5 && j==1 || i==5 && j==2 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Cut)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Cut)&nbsp;</b></td>");															
								}	
							}

							//LH BEOLS Input/Output Chip
							if( i==6 && j==1 || i==6 && j==2 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Chip)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Chip)&nbsp;</b></td>");															
								}	
							}

							//LH BEOLL Input/Output Chip
							if( i==7 && j==1 || i==7 && j==2 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Chip)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Chip)&nbsp;</b></td>");															
								}	
							}

							//CN_STN Input/Output Chip
							if( i==8 && j==1 || i==8 && j==2 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Chip)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Chip)&nbsp;</b></td>");															
								}	
							}

							//F-CTP Input/Output Chip
							if( i==9 && j==1 || i==9 && j==2 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Sheet)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Sheet)&nbsp;</b></td>");															
								}	
							}

							//F-CTP Input/Output Chip
							if( i==10 && j==1 || i==10 && j==2 )
							{
								if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
								{													
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "(Chip)</font>" + "&nbsp;</b></td>");								
								}
								else
								{																							
									sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"(Chip)&nbsp;</b></td>");															
								}	
							}


							/*
							if( Convert.ToInt32(ds.Rows[i][j].ToString()) < 0 )
							{													
								sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ "<font color=red>" + ds.Rows[i][j].ToString() + "</font>" + "&nbsp;</b></td>");								
							}
							else
							{																							
								sb.Append("<td font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"&nbsp;</b></td>");															
							}
							//*/						
						}						
					}
					
				}
				sb.Append("</tr>");
				
			}
			sb.Append("</table>");			
			return sb.ToString();
		}// end of gethtml_2

		//for STN MSG
		//Bunny 2009-05-09
		public  string gethtml_3(DataTable ds)
		{

			// D5D291 ==> 8080FF =>C0CBFE => DDE3FF
			StringBuilder sb=new StringBuilder();
			//sb.Append("<table border=1 background-color=#D5D291;border-bottom= 1px solid #DDE3FF; border-right=1px solid #DDE3FF;border-left=1px solid #DDE3FF;border-top=1px solid #ffffff;   width=80%   cellspacing=1 cellpadding=1  >");
			sb.Append("<table border=1 width=80% cellspacing=1 cellpadding=1>");
			sb.Append("<tr>") ;
			foreach(DataColumn myCol in ds.Columns)
			{
				sb.Append("<td bgcolor=#0000FF font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;>&nbsp;" + "<font color=white><b>" + myCol.Caption + "</b></font>" + "&nbsp;</td>");
			}
			sb.Append("</tr>") ;
			for(int i=0;i<ds.Rows.Count;i++)
			{

				sb.Append("<tr>");
				for(int j=0;j<ds.Columns.Count;j++)
				{
					if ( j == 0 )
					{
						sb.Append("<td bgcolor=#B5DAFF font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;>&nbsp;"+ds.Rows[i][j].ToString()+"&nbsp;</td>");
					}					
					else
					{
						sb.Append("<td bgcolor=# font-size=11px;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+ds.Rows[i][j].ToString()+"&nbsp;</b></td>");																	
					}					
				}
				sb.Append("</tr>");
				
			}
			sb.Append("</table>");
			return sb.ToString();
		}//end of gethtml_3
		
		



	}
}
