using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
//using System.Data.OracleClient;
using System.Configuration;
using System.Web;

//namespace oeeweb2.util
//{
	/// <summary>
	/// db 的摘要描述。
	/// </summary>
	public class db
	{
		public db()
		{
			//
			// TODO: 在此加入建構函式的程式碼
			//
		}

		const string MSG_REGULAR_COLOR = "blue";
		const string MSG_ERROR_COLOR = "red";
		const string MSG_SQL_INS_DONE = "<b>新增資料:</b>資料已成功的加入資料庫<br>";
		const string MSG_SQL_DEL_DONE = "<b>刪除資料:</b>資料已成功的由資料庫中刪除<br>" ;
		const string MSG_SQL_UPT_DONE = "<b>資料修改:</b>資料已成功的在資料庫中修改<br>";
		const string MSG_SQL_ERROR_DUPLICATED_KEY = "ERROR: 代碼重複, 請重新輸入<br>"; 
		const string MSG_SQL_ERROR_OTHER_PREFIX = "ERROR: ";
   
		public static void ErrorHandler(Exception Ex, Label Message)
		{
			Message.Style["font-size"] ="12pt";
			Message.Style["color"] = "red";
			Message.Text= "ERROR:" + Ex.Message;    
		}

		public static void SQLErrorHandler(System.Data.OleDb.OleDbException E,Label Message )
		{
			try
			{
				Message.Style["font-size"] ="18pt";
				Message.Style["color"] = MSG_ERROR_COLOR ;
				switch (E.ErrorCode) 
				{
					case -2147217873:
						Message.Text+= MSG_SQL_ERROR_DUPLICATED_KEY;
						break;
					default:
						Message.Text+= MSG_SQL_ERROR_OTHER_PREFIX +"[" +E.ErrorCode.ToString()+"]"+ E.ToString();	   
						break;	
				}	
			}
			catch (System.Exception e)
			{
				ErrorHandler(e, Message);
				return ;
			}
		}
 
		public static void MessageSQLInsDone(Label Message) 
		{
			try
			{
				Message.Style["font-size"] ="10pt";
				Message.Style["color"] = MSG_REGULAR_COLOR;
				Message.Text+= MSG_SQL_INS_DONE;
			}
			catch (System.Exception e)
			{
				ErrorHandler(e, Message);
			}
		}

		public static void MessageSQLUptDone(Label Message) 
		{
			try
			{
				Message.Style["font-size"] ="10pt";
				Message.Style["color"] = MSG_REGULAR_COLOR;
				Message.Text+= MSG_SQL_UPT_DONE;
			}
			catch (System.Exception e)
			{
				ErrorHandler(e, Message);
			}
		}

		public static void MessageSQLDelDone( Label Message) 
		{
			try
			{
				Message.Style["font-size"] ="10pt";
				Message.Style["color"] = MSG_REGULAR_COLOR;
				Message.Text+= MSG_SQL_DEL_DONE;
			}
			catch (System.Exception e)
			{
				ErrorHandler(e, Message);
			}
		}

		public static string GetContextRoot(System.Web.HttpContext Context) 
		{
			if(Context.Request.ApplicationPath=="/") 
			{
				return "";
			}
			else
			{
				return Context.Request.ApplicationPath; 	
			}
		}
		/// <summary>
		/// Get web_config db string
		/// </summary>
		/// <param name="dbname"></param>
		/// <returns></returns>
		private static string getConnection(string S_dbname)
		{
			return System.Configuration.ConfigurationSettings.AppSettings[S_dbname];
		}
		/// <summary>
		/// DB
		/// 1.OEE
		/// 2.SSO
		/// </summary>
		/// <param name="strSql"></param>
		/// <param name="S_dbname"></param>
		/// <returns></returns>
		public static DataSet GetDataset(string strSql,int i) 
		{
			string S_dbname="";
			switch(i)
			{
                case 1:
                    S_dbname = "DBCONN_ARY";
                    break;
                case 2:
                    S_dbname = "DBCONN_CEL";
                    break;
                case 3:
                    S_dbname = "DBCONN_CFT";
                    break;
                case 4:
                    S_dbname = "DBCONN_EDA";
                    break;
                case 5:
                    S_dbname = "DBCONN_SSODB_OLE";
                    break;
                case 6:
                    S_dbname = "DBCONN_APS";
                    break;
                case 7:
                    S_dbname = "DBCONN_ARY_STDMAN";
                    break;
                case 8:
                    S_dbname = "DBCONN_MISDB2";
                    break;
                case 9:
                    S_dbname = "DBCONN_PARS1";
                    break;
                case 10:
                    S_dbname = "DBCONN_POEE";
                    break;
                case 11:
                    S_dbname = "DBCONN_OARPT_OLE";
                    break;
			}

			DataSet ds = new DataSet();
			OleDbConnection myConnection = new OleDbConnection();
			OleDbCommand myCommand=new OleDbCommand();
			OleDbDataAdapter mydataad = new OleDbDataAdapter();
			try
			{
				
				myConnection.ConnectionString=getConnection(S_dbname);
				myCommand.CommandType=CommandType.Text;
				myCommand.Connection=myConnection;
				myCommand.CommandText=strSql;

				myConnection.Open();
				mydataad.SelectCommand=myCommand;
				mydataad.Fill(ds);
				myConnection.Close();
			}
			catch(Exception ex)
			{
			string a=ex.ToString();
			}
			
			return ds;
		}

		public static OleDbDataReader GetUserInfo(string strSql,int i) 
		{
			string S_dbname="";
			switch(i)
			{
				case 1:
					S_dbname="DBCONN_OEE";
					break;
				case 2:
					S_dbname="DBCONN_SSO";
					break;
			}
			
			OleDbConnection myConnection = new OleDbConnection();
			OleDbCommand myCommand=new OleDbCommand();
			OleDbDataReader myReader;

			try
			{
				myConnection.ConnectionString=getConnection(S_dbname);
				myCommand.CommandType=CommandType.Text;
				myCommand.Connection=myConnection;
				myCommand.CommandText=strSql;
				myConnection.Open();
			}
			catch
			{
			
			}
			myReader= myCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return myReader;
		}

		//public static OleDbDataReader GetDatareader(string strSql,int i) 
		//{
		//	string S_dbname="";
		//	switch(i)
		//	{
		//		case 1:
		//			S_dbname="DBCONN_OEE";
		//			break;
		//		case 2:
		//			S_dbname="DBCONN_SSO";
		//			break;
		//	}
		//	
		//	OleDbConnection myConnection = new OleDbConnection();
		//	OleDbCommand myCommand=new OleDbCommand();
		//	OleDbDataReader myReader;
		//
		//	try
		//	{
		//		myConnection.ConnectionString=getConnection(S_dbname);
		//		myCommand.CommandType=CommandType.Text;
		//		myCommand.Connection=myConnection;
		//		myCommand.CommandText=strSql;
		//		myConnection.Open();
		//	}
		//	catch
		//	{
		//	
		//	}
		//	myReader= myCommand.ExecuteReader(CommandBehavior.CloseConnection);
		//	return myReader;
		//}

		public static void ExecuteStatement(string strSql,int i) 
		{
			string S_dbname="";
			switch(i)
			{
                case 1:
                    S_dbname = "DBCONN_ARY";
                    break;
                case 2:
                    S_dbname = "DBCONN_CEL";
                    break;
                case 3:
                    S_dbname = "DBCONN_CFT";
                    break;
                case 4:
                    S_dbname = "DBCONN_EDA";
                    break;
                case 5:
                    S_dbname = "DBCONN_SSO";
                    break;
                case 6:
                    S_dbname = "DBCONN_APS";
                    break;
                case 7:
                    S_dbname = "DBCONN_ARY_STDMAN";
                    break;
                case 8:
                    S_dbname = "DBCONN_MISDB2";
                    break;
                case 9:
                    S_dbname = "DBCONN_PARS1";
                    break;
                case 10:
                    S_dbname = "DBCONN_POEE";
                    break;
                case 11:
                    S_dbname = "DBCONN_OARPT_OLE";
                    break;
			}
			OleDbConnection myConnection = new OleDbConnection(getConnection(S_dbname));
			OleDbCommand myCommand =new OleDbCommand(strSql, myConnection);
			myConnection.Open();
			myCommand.ExecuteNonQuery();
			myConnection.Close();
		}
		public static string Execute_Scalar(string strSql,int i) 
		{
			string S_dbname="";
			string Scalar="";
			switch(i)
			{
				case 1:
					S_dbname="DBCONN_OEE";
					break;	
			}
			OleDbConnection myConnection = new OleDbConnection(getConnection(S_dbname));
			OleDbCommand myCommand =new OleDbCommand(strSql, myConnection);
			myConnection.Open();
			Scalar=myCommand.ExecuteScalar().ToString();
			myConnection.Close();
			return Scalar;
			
		}


		public static DataTable ConvertTable(DataTable source_dt)
		{
		     
			
			 DataTable target_dt = new DataTable () ;
			 
			if (source_dt.Rows.Count == 0 )
				return source_dt ; 
			
			object[] rowVals = new object[source_dt.Rows.Count-1];

             int i = 0 , j = 0;

			for ( i = 0 ; i <= source_dt.Rows.Count ; i ++ )
			{   if (i == 0 ) 
                   target_dt.Columns.Add("/") ;
				else
				   target_dt.Columns.Add(source_dt.Rows[i-1][0].ToString()) ;
			}

			for ( i =1 ; i <= source_dt.Columns.Count-1 ; i ++ )
				target_dt.Rows.Add(rowVals) ; 

             
			for(i = 0 ; i < target_dt.Rows.Count ; i++ )
			{
				
                target_dt.Rows[i][0] = source_dt.Columns[i+1].ColumnName ;

				for(j = 1 ; j < target_dt.Columns.Count ; j++ )
				{
					target_dt.Rows[i][j] = source_dt.Rows[j-1][i+1] ;
				}
			}

             
             return target_dt ;
		}
	
	
	
	
	}
//}
