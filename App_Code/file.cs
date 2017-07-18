using System;
using System.IO;
using System.Collections;
using System.Xml;
using System.Data;

namespace IS.util
{
	/// <summary>
	/// file 的摘要描述。
	/// </summary>
	public class file
	{
		public file()
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
		public void Create_ALCS_xml(string file_path,string file_name,ArrayList Element,ArrayList Element_txt)
		{
			XmlTextWriter xmlw=new XmlTextWriter(file_path+"\\"+file_name,null);
			xmlw.Formatting=Formatting.Indented;
			xmlw.Indentation=3;
			xmlw.WriteStartDocument();
			xmlw.WriteStartElement("ALARMCONTENT");

			for(int i=0;i<Element.Count;i++)
			{

				xmlw.WriteElementString(Element[i].ToString(),Element_txt[i].ToString());
			
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
