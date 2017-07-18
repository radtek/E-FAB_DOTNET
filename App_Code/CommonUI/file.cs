using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Collections;


namespace Innolux.Portal.CommonFunction
{
	/// <summary>
	/// file 的摘要描述。
	/// </summary>
    /// 
    public class CommonFolder
    {
        #region "For folder operation"
        private string _strErrorMessage = "";

        public CommonFolder()
		{
			//
			// TODO: 在此加入建構函式的程式碼
			//
		}
        public bool CreateSubFolder(string path, string strFolder)
        {
            try
            {
                string strFolderpath = "~/" + path;
                strFolderpath = HttpContext.Current.Server.MapPath(strFolderpath);
                if (Directory.Exists(strFolderpath) == true)
                {
                    Directory.CreateDirectory(strFolderpath + "\\" + strFolder);
                }
                else
                {
                    _strErrorMessage = "The folder [" + strFolderpath + "] not exist!";
                }
                return (true);
            }
            catch (Exception ex)
            {
                _strErrorMessage = ex.Message;
                return (false);
            }
        }

        public bool CreateSubFolder(string SubFolderName)
        {
            try
            {
                string strFolder = GetRootFolder();
                Directory.CreateDirectory(strFolder + @SubFolderName);
                return (true);
            }
            catch (Exception ex)
            {
                _strErrorMessage = ex.Message;
                return (false);
            }
        }

        public bool RemoveFolder(string strFolder,bool IsCludeSubFolder)
        {
            try
            {
                if (FolderExist(strFolder) == true)
                {
                    Directory.Delete(strFolder, IsCludeSubFolder);
                }
                return (true);
            }
            catch (Exception ex)
            {
                _strErrorMessage = ex.Message;
                return (false);
            }
        }

        private string GetRootFolder()
        {
            string strPath = "";
            strPath = HttpContext.Current.Server.MapPath("~/Emeeting/meetingfiles");
            return (strPath);
        }
        public bool FolderExist(string strFolder)
        {
            string strFolderpath = "";
            if (strFolder.IndexOf("/") < 0)
            {
                strFolderpath = "~/" + strFolder;
                strFolderpath = HttpContext.Current.Server.MapPath(strFolderpath);
            }
            else
            {
                strFolderpath = HttpContext.Current.Server.MapPath(strFolder);
            }

            return (Directory.Exists(strFolderpath));
        }

        #endregion



    }


	public class Commonfile
	{
        private CommonFolder myFolder = new CommonFolder();
        private StreamWriter m_objStreamWriter = null;

        public Commonfile()
		{
			//
			// TODO: 在此加入建構函式的程式碼
			//
		}
		private void Createlog(string shop,string msg)
		{
			string path=@"D:\Copyfile_log\"+shop+"_"+DateTime.Now.ToShortDateString().Replace("/","")+".txt";
			FileInfo fil=new FileInfo(path);
			if (!fil.Exists) 
			{
								
				using (StreamWriter sw = fil.CreateText()) 
				{
					sw.WriteLine("System Log file");
					sw.Close();
				}    
			}
			using (StreamWriter sw=fil.AppendText())
			{
				sw.WriteLine(DateTime.Now.ToLongTimeString());
				sw.WriteLine(msg);
				sw.Close();
			}
		}

        public void Writelog(string shopName, string msgTitle, string msgDesc,string page,string strSql)
        {
            //string path = @"D:\Copyfile_log\" + shop + "_" + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";
            string Folder = "LogFiles";
            if (myFolder.FolderExist(Folder) == false)
            {
                myFolder.CreateSubFolder("", Folder);
            }
            string logFilePath = HttpContext.Current.Server.MapPath(@"~\LogFiles\" + shopName + "_" + ChangeDateStringToNum(DateTime.Now.ToShortDateString()) + ".txt");
            if (File.Exists(logFilePath) == true)
            {
                File.SetAttributes(logFilePath, FileAttributes.Normal);
            }
            m_objStreamWriter = new StreamWriter(logFilePath,true,System.Text.Encoding.Default);
            m_objStreamWriter.WriteLine("");
            m_objStreamWriter.WriteLine("********************************************Start*******************************");
            m_objStreamWriter.WriteLine("DateTime:" + DateTime.Now.ToLongTimeString());
            m_objStreamWriter.WriteLine("Title:   " + msgTitle);
            m_objStreamWriter.WriteLine("Desc:    " + msgDesc);
            m_objStreamWriter.WriteLine("Page:    " + page);
            m_objStreamWriter.WriteLine("Sql:     " + strSql);
            m_objStreamWriter.WriteLine("*********************************************End*********************************");
            m_objStreamWriter.Close();
            m_objStreamWriter = null;
            
        }


        public string ChangeDateStringToNum(string DateString)
        {
            DateTime dtVar = System.DateTime.Now;
            string strReturn = "";
            string strYear = "";
            string strMonth = "";
            string strDay = "";
            dtVar = Convert.ToDateTime(DateString);
            strYear = dtVar.Year.ToString();
            strMonth = dtVar.Month.ToString();
            strDay = dtVar.Day.ToString();
            if (strMonth.Length == 1)
            {
                strMonth = "0" + strMonth;
            }
            if (strDay.Length == 1)
            {
                strDay = "0" + strDay;
            }
            strReturn = strYear + strMonth + strDay;
            return (strReturn);
        }


		private void file_delete(DirectoryInfo di,FileInfo[] fi,string root_path,string ftp_path)
		{
			string _sfile_nmae;
			DateTime dt_s;
			dt_s=DateTime.Now;
			_sfile_nmae="";
			
			int i=0;
			foreach (FileInfo fiTemp in fi)
			{
				if(i==0)
				{
					dt_s=fiTemp.CreationTime;
					_sfile_nmae=fiTemp.Name;
					
				}
				else
				{
					if(fiTemp.CreationTime>dt_s)
					{
						try
						{
							fiTemp.MoveTo(ftp_path.Replace(di.Name,"bak\\")+i.ToString()+_sfile_nmae);
						}
						catch
						{
							fiTemp.MoveTo(ftp_path.Replace(di.Name,"bak\\")+DateTime.Now.Millisecond.ToString()+_sfile_nmae);
						}
						_sfile_nmae=fiTemp.Name;
						dt_s=fiTemp.CreationTime;
					}
					else
					{
						fiTemp.CopyTo(ftp_path.Replace(di.Name,"bak\\")+fiTemp.Name,true);
						fiTemp.Delete();
					}
					
				}
				i=i+1;
				
				
			}
			
		
		}
		public bool file_exists(string file_path)
		{
		FileInfo f=new FileInfo(file_path);
		return f.Exists;
		
		}
		public void File_del(string file_path)
		{
		FileInfo f=new FileInfo(file_path);
		f.Delete();
		
		
		}
		public DataSet Read_xml(string file_path)
		{
			FileStream fsReadXml = new FileStream(file_path,FileMode.Open);
		    XmlTextReader myXmlReader =new XmlTextReader(fsReadXml);
			DataSet newDataSet = new DataSet("New DataSet");
			newDataSet.ReadXml(myXmlReader);
			myXmlReader.Close();
			return newDataSet;
		
		}
		public void Create_easy_xml(string file_path,string file_name,string Xml_root,ArrayList Element,ArrayList Element_txt)
		{
			XmlTextWriter xmlw=new XmlTextWriter(file_path+"\\"+file_name+".xml",null);
			xmlw.Formatting=Formatting.Indented;
			xmlw.Indentation=3;
			xmlw.WriteStartDocument();
			xmlw.WriteStartElement(Xml_root);
			for(int i=0;i<Element.Count;i++)
			{

			xmlw.WriteAttributeString(Element[i].ToString(),Element_txt[i].ToString());
			
			}
											
			
			xmlw.WriteEndElement();
			xmlw.Flush();
			xmlw.Close();

		
		
		}
		private void Create_xml(string user_id,string path)
		{
			XmlTextWriter xmlw=new XmlTextWriter(path+"\\"+user_id+".xml",null);
			xmlw.Formatting=Formatting.Indented;
			xmlw.Indentation=3;
			xmlw.WriteStartDocument();
			xmlw.WriteComment("TEST XML");
			xmlw.WriteStartElement("BOOK_list");
			xmlw.WriteStartElement("BOOK");
			xmlw.WriteAttributeString("Category","Technology");
			xmlw.WriteAttributeString("PageCount","1234");
		
			xmlw.WriteElementString("Title","DB");
			xmlw.WriteElementString("RELEASE","AA");

			xmlw.WriteStartElement("AuthorList");
			xmlw.WriteAttributeString("Category_1","Technology_1");
			xmlw.WriteAttributeString("PageCount_1","1235");
			xmlw.WriteElementString("Author","Mark");
			xmlw.WriteElementString("Author","imer");

			//close AuthorList
			xmlw.WriteEndElement();
			//close BOOK
			xmlw.WriteEndElement();
			////close BOOK_list
			xmlw.WriteEndElement();
			xmlw.Flush();
			xmlw.Close();
		
		
		
		}
		
	}
}
