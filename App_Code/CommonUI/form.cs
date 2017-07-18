using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;


namespace Innolux.Portal.CommonFunction
{
	/// <summary>
	/// form 的摘要描述。
	/// </summary>
	public class form
	{
		public form()
		{
			//
			// TODO: 在此加入建構函式的程式碼
			//
		}
		public void Set_Profile_str(Button bt,bool b)
		{
			if(b)
			{
				bt.Text="MEMORY";
				bt.ToolTip="記憶偏好選項,作為每次登入預設選則條件";
			}
			else
			{
				bt.Text="CLEAN";
				bt.ToolTip="清除個人儲存記憶選項";
			}
		
		}

		public void GetmonthFromToDropDownList(DropDownList datefromDropDownList,DropDownList datetoDropDownList)
		{
			DateTime endDate=Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM") + "/01");
			DateTime startDate=Convert.ToDateTime(DateTime.Now.ToString("yyyy") + "/01/01").AddYears(-1);

			while (DateDiff("month",startDate,endDate) >= 0)
			{
				ListItem DropDownListItem1=new ListItem();
				DropDownListItem1.Text = endDate.ToString("yyyy/MM"); 
				DropDownListItem1.Value = endDate.ToString("yyyyMM"); 
				datefromDropDownList.Items.Add(DropDownListItem1);
				datetoDropDownList.Items.Add(DropDownListItem1);
				endDate=endDate.AddMonths(-1).Date;
			}

		}
		public void GetmonthDropDownList(DropDownList dateDropDownList)
		{
			DateTime endDate=Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM") + "/01");
			DateTime startDate=Convert.ToDateTime(DateTime.Now.ToString("yyyy") + "/01/01").AddYears(-1);

			while (DateDiff("month",startDate,endDate) >= 0)
			{
				ListItem DropDownListItem1=new ListItem();
				DropDownListItem1.Text = endDate.ToString("yyyy/MM"); 
				DropDownListItem1.Value = endDate.ToString("yyyyMM"); 
				dateDropDownList.Items.Add(DropDownListItem1);
				endDate=endDate.AddMonths(-1).Date;
			}

		}
		public string GetWeekOfYear(DateTime sDate)
		{
			string weekofyear;
			GregorianCalendar c = new GregorianCalendar(GregorianCalendarTypes.Localized); 
			int weeknr = c.GetWeekOfYear(sDate,CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Tuesday); 
   
			weekofyear= "0" + weeknr.ToString();
			if (weekofyear.Length == 3)
				weekofyear="W" +weekofyear.Substring(1,2);
			else
				weekofyear="W" +weekofyear;
			return weekofyear;
		}

		public void GetweekFromToDropDownList(DropDownList datefromDropDownList,DropDownList datetoDropDownList)
		{
			DateTime endDate;
			DateTime startDate;
   
			endDate=DateTime.Now;
			int j = 0;
			while (endDate.AddDays(-j).Date.DayOfWeek.ToString()!="Tuesday")
			{
				j++;
			}
			startDate = endDate.AddDays(-j).Date;
 
			ListItem DropDownListItem1=new ListItem();
			DropDownListItem1.Text=startDate.Year.ToString() + GetWeekOfYear(startDate).ToString() + " (" + startDate.ToString("yyyy/MM/dd") + "~" + endDate.ToString("yyyy/MM/dd") + ")";
			DropDownListItem1.Value = startDate.ToString("yyyyMMdd") + endDate.ToString("yyyyMMdd");
			datefromDropDownList.Items.Add(DropDownListItem1);
			datetoDropDownList.Items.Add(DropDownListItem1);

			for (j=1;j<=49;j++)
			{
				endDate=startDate.AddDays(-1).Date;
				startDate=startDate.AddDays(-7).Date;

				DropDownListItem1=new ListItem();
				DropDownListItem1.Text=startDate.Year.ToString() + GetWeekOfYear(startDate).ToString() + " (" + startDate.ToString("yyyy/MM/dd") + "~" + endDate.ToString("yyyy/MM/dd") + ")";
				DropDownListItem1.Value = startDate.ToString("yyyyMMdd") + endDate.ToString("yyyyMMdd");
				datefromDropDownList.Items.Add(DropDownListItem1);
				datetoDropDownList.Items.Add(DropDownListItem1);
			}
		}
		public void GetweekDropDownList(DropDownList dateDropDownList)
		{
			DateTime endDate;
			DateTime startDate;
   

			endDate=DateTime.Now;
			int j = 0;
			while (endDate.AddDays(-j).Date.DayOfWeek.ToString()!="Tuesday")
			{
				j++;
			}
			startDate = endDate.AddDays(-j).Date;
 
			ListItem DropDownListItem1=new ListItem();
			DropDownListItem1.Text=startDate.Year.ToString() + GetWeekOfYear(startDate).ToString() + " (" + startDate.ToString("yyyy/MM/dd") + "~" + endDate.ToString("yyyy/MM/dd") + ")";
			DropDownListItem1.Value = startDate.ToString("yyyyMMdd") + endDate.ToString("yyyyMMdd");
			dateDropDownList.Items.Add(DropDownListItem1);

			for (j=1;j<=49;j++)
			{
				endDate=startDate.AddDays(-1).Date;
				startDate=startDate.AddDays(-7).Date;

				DropDownListItem1=new ListItem();
				DropDownListItem1.Text=startDate.Year.ToString() + GetWeekOfYear(startDate).ToString() + " (" + startDate.ToString("yyyy/MM/dd") + "~" + endDate.ToString("yyyy/MM/dd") + ")";
				DropDownListItem1.Value = startDate.ToString("yyyyMMdd") + endDate.ToString("yyyyMMdd");
				dateDropDownList.Items.Add(DropDownListItem1);


			}
            
		}
		/// <summary>
		/// textbox get start_day/end_day  format(1):yyyy/mm/dd format(2):yyyymmdd
		/// </summary>
		/// <param name="tx1"></param>
		/// <param name="tx2"></param>
		public void Textbox_getday(TextBox tx1,TextBox tx2,int format)
		{
			
			switch(format)
			{
				case 1:
					tx1.Text=DateTime.Today.ToString("yyyy/MM/dd");
					tx2.Text=DateTime.Today.AddDays(1).ToString("yyyy/MM/dd");
					break;
				case 2:
					tx1.Text=DateTime.Today.ToString("yyyyMMdd");
					tx2.Text=DateTime.Today.AddDays(1).ToString("yyyyMMdd");
					
					break;
			
			
			
			}
			
			
		
		}
		/// <summary>
		/// textbox get start_day format(1):yyyy/mm/dd format(2):yyyymmdd
		/// </summary>
		/// <param name="tx1"></param>
		/// <param name="tx2"></param>
		public void Textbox_getday(TextBox tx1,int format)
		{
			switch(format)
			{
				case 1:
					tx1.Text=DateTime.Today.ToString("yyyy/MM/dd");
					
					break;
				case 2:
					tx1.Text=DateTime.Today.ToString("yyyyMMdd");
					
					
					break;
			
			
			
			}
				
		}
		public double DateDiff(string howtocompare, System.DateTime startDate, System.DateTime endDate) 
		{ 
			double diff=0; 
			System.TimeSpan TS = new System.TimeSpan(endDate.Ticks-startDate.Ticks); 

			switch (howtocompare.ToLower()) 
			{ 
				case "year": 
					diff = Convert.ToDouble(TS.TotalDays/365); 
					break; 
				case "month": 
					diff = Convert.ToDouble((TS.TotalDays/365)*12); 
					break; 
				case "day":
					diff = Convert.ToDouble(TS.TotalDays); 
					break; 
				case "hour": 
					diff = Convert.ToDouble(TS.TotalHours); 
					break; 
				case "minute": 
					diff = Convert.ToDouble(TS.TotalMinutes); 
					break; 
				case "second": 
					diff = Convert.ToDouble(TS.TotalSeconds); 
					break; 
			}

			return diff;
		}  

		/// <summary>
		/// Retun List_box selected: return ex:1,2,3
		/// </summary>
		/// <param name="dscol"></param>
		/// <returns></returns>
		public string GetStringList(ListBox dscol) 
		{
			string var = "";
			for (int i = 0; i <= dscol.Items.Count - 1; i++)
			{
				if (dscol.Items[i].Selected)
				{
					var = var +"'"+ dscol.Items[i].Value +"'" +",";
				}
			}
			var = var.Substring(0,var.Length-1);
			return var;
		}
		/// <summary>
		/// colname_1=DataValueField
		/// colname_2=DataTextField
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="dl"></param>
		/// <param name="colname_1"></param>
		/// <param name="colname_2"></param>
		public void ListBox_Bind(DataSet ds,ListBox dl,string colname_1,string colname_2)
		{
			dl.Items.Clear();
			dl.DataSource=ds.Tables[0].DefaultView;
			dl.DataValueField=colname_1;
			dl.DataTextField=colname_2;
			dl.DataBind();
			ds.Clear();
		
		}
		public void DropList_Bind(DataSet ds,DropDownList dl,string colname_1,string colname_2)
		{
			dl.Items.Clear();
			dl.DataSource=ds.Tables[0].DefaultView;
			dl.DataValueField=colname_1;
			dl.DataTextField=colname_2;
			dl.DataBind();
			ds.Clear();
		
		}
		public void DropList_Bind(ArrayList al,DropDownList dl)
		{
			dl.Items.Clear();
			
			for(int i=0;i<al.Count;i++)
			{
			dl.Items.Add(al[i].ToString());			
			}

		
		}
		public void DropList_Bind(DataSet ds,DropDownList dl,string colname_1)
		{
			dl.Items.Clear();
			dl.DataSource=ds.Tables[0].DefaultView;
			dl.DataValueField=colname_1;
			dl.DataTextField=colname_1;
			dl.DataBind();
			ds.Clear();
		
		}
		/// <summary>
		/// i=1 shop="T0ARRAY/T1ARRAY" i=2 shop="T1CELL/T0CELL" i=3 shop="T1CF" i=4 shop="T0ARRAY/T1ARRAY/T1CELL/T0CELL/T1CF"
		/// </summary>
		/// <param name="dl"></param>
		/// <param name="i"></param>
		public void Drop_list_shop(DropDownList dl,int i)
		{
		
		dl.Items.Clear();
		switch(i)
			{
				case 1:
				dl.Items.Add("T0AARY");
				dl.Items.Add("T1AARY");
					break;
                case 2:
				dl.Items.Add("T0CELL");
				dl.Items.Add("T1CELL");
					break;

				case 3:
				dl.Items.Add("T1CF");
					break;

				case 4:
				dl.Items.Add("T0AARY");
				dl.Items.Add("T1AARY");
				dl.Items.Add("T0CELL");
				dl.Items.Add("T1CELL");
				dl.Items.Add("T1CF");

					break;
			
			
			
			
			
			}
		
		
		}
		/// <summary>
		/// P/E/MQC
		/// </summary>
		/// <param name="dl"></param>
		public void Drop_list_lottype(DropDownList dl)
		{
		dl.Items.Clear();
		dl.Items.Add("P");
		dl.Items.Add("E");
		dl.Items.Add("MQC");

		
		}
		/// <summary>
		/// TFT/CF
		/// </summary>
		/// <param name="dl"></param>
		public void Drop_list_glasstype(DropDownList dl)
		{
			dl.Items.Clear();
			dl.Items.Add("TFT");
			dl.Items.Add("CF");
		
		}
		public void Datagrid_Bind(DataSet ds,DataGrid dg)
		{
			dg.DataSource=ds.Tables[0].DefaultView;
			dg.DataBind();		
		
		}
		public void ExportExcel(DataGrid SeriesValuesDataGrid) 
		{
			System.Web.UI.Control ctl = new System.Web.UI.Control();
			HttpContext.Current.Response.ContentType = "application/ms-Excel";
			HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
			System.IO.StringWriter tw = new System.IO.StringWriter();
			System.Web.UI.Html32TextWriter hw = new System.Web.UI.Html32TextWriter(tw);
			ctl.EnableViewState = false;
			SeriesValuesDataGrid.RenderControl(hw);
			HttpContext.Current.Response.Write(tw.ToString());
			HttpContext.Current.Response.End();  
		}
		public  void  Windowsopen(string url,string name)
		{
			string stoolbar="width=800,height=768,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no";
			//string s="<script language=javascript> open('"+shttp+"','"+sname+"','"+stoolbar+"')</script>";
			System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>" + "open('" + url + "','"+name+"','"+stoolbar+"')" + "</SCRIPT>");
		}
		public void MsgBox(string message)
		{
			System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>" + "alert('" + message + "')" + "</SCRIPT>");
		}

		/// <summary>
		/// Adjust ListBox items,Adjust Selected items to Top
		/// </summary>
		/// <param name="productListbox"></param>
		public void AdjustItems(ListBox productListbox)
		{
			ArrayList selectedList = new ArrayList();
			ArrayList allList = new ArrayList();
			for (int i = 0; i <= productListbox.Items.Count - 1; i++)
			{
				if (productListbox.Items[i].Selected)
				{
					selectedList.Add(productListbox.Items[i].Value);
				}
				allList.Add(productListbox.Items[i].Value);
			}
			selectedList.Sort();
			string[] allary = (string[])allList.ToArray(typeof(string));
			string[] selectedary = (string[])selectedList.ToArray(typeof(string));
			productListbox.Items.Clear();
			//add selected item
			for(int i=0; i <= selectedary.Length-1; i++) 
			{
				productListbox.Items.Add(selectedary[i]) ;
				productListbox.Items[i].Selected = true;
			}
			//add not selected item
			for(int j=0; j <= allary.Length-1; j++) 
			{
				if (selectedList.Contains(allary[j])==false)
					productListbox.Items.Add(allary[j]) ;
			}
		}
	
		/// <summary>
		/// Input html_table & runat=server 
		/// </summary>
		/// <param name="ta"></param>
		/// <returns></returns>
		public ArrayList Find_table_cn(HtmlTable ta,bool Table_Style_set)
		{
			ArrayList al=new ArrayList();
			
			if(Table_Style_set)
			{
				ta.Border=2;
				ta.BorderColor="A1F1F1";
				//ta.BgColor="ffffff"; 
				ta.Width="950";
			}
		
			
			for(int i=0;i<ta.Rows.Count;i++)
			{
				for(int j=0;j<ta.Rows[i].Cells.Count;j++)
				{
					for(int f=0;f<ta.Rows[i].Cells[j].Controls.Count;f++)
					{						
						
						if(ta.Rows[i].Cells[j].Controls[f].ToString().StartsWith("System.Web.UI.WebControls"))
						{

						al.Add(ta.Rows[i].Cells[j].Controls[f]);
						
						}
						
						
					}				
				}				
			}
			return al;
		
		
		
		}
		
		
		/// <summary>
		/// button 
		/// s:save
		/// c:cancel
		/// e:edit
		/// q:query
		/// cl:clean
		/// m:memory
		/// Label
		/// s:shop
		/// l:lottype
		/// g:glasstype
		/// sd:start_dttm
		/// ed:end_dttm
		/// vb:view by
		/// pv:PREVIEW
		/// dv:DRILLDOWN VIEW
		/// sf:SHIFT_DATE
		/// t:Report_Name
		/// TextBox:set style
		/// DropDownList:set style
		/// DataGrid
		/// CheckBox:set style 
		/// e:ENABLE3D
		/// </summary>
		/// <param name="o"></param>
		public void Set_style(object o)
		{
			switch(o.ToString())
			{
				case "System.Web.UI.WebControls.Button":
				Button bt;
				bt=(Button)o;
									
					if(bt.ClientID.ToLower().EndsWith("s"))
					{
						bt.Text="SAVE";
					}
					else if(bt.ClientID.ToLower().EndsWith("c"))
					{
					
						bt.Text="CANCEL";
					}
					else if(bt.ClientID.ToLower().EndsWith("e"))
					{
					
						bt.Text="EDIT";
					}
					else if(bt.ClientID.ToLower().EndsWith("q"))
					{
					
						bt.Text="QUERY";
					}
					else if(bt.ClientID.ToLower().EndsWith("cl"))
					{
					
						bt.Text="CLEAN";
						bt.ToolTip="清除個人儲存記憶選項";
					}
					else if(bt.ClientID.ToLower().EndsWith("m"))
					{
					
						bt.Text="MEMORY";
						bt.ToolTip="記憶偏好選項,作為每次登入預設選則條件";
					}
					else
					{
					
						
					}

				bt.CssClass="btnhov";
				bt.Width=100;
				bt.Height=25;

				break;

				case "System.Web.UI.WebControls.TextBox":
				TextBox tx;
				
				tx=(TextBox)o;
				tx.CssClass="XPSilverText";
				tx.Width=150;
				tx.Height=25;

				break;

				case "System.Web.UI.WebControls.ListBox":
					ListBox lx;
				
					lx=(ListBox)o;
					lx.CssClass="XPSilverText";
					lx.Width=150;
					lx.Height=75;
					
					if(lx.ClientID.ToLower().EndsWith("s"))
					{
					lx.SelectionMode=ListSelectionMode.Single;
					}
					else
					{
					lx.SelectionMode=ListSelectionMode.Multiple;
					
					}


					break;
				case "System.Web.UI.WebControls.CheckBox":
					CheckBox ck;
					ck=(CheckBox)o;
					ck.CssClass="ParamTblTitle";
					ck.Width=100;
					ck.Height=25;
					ck.TextAlign=TextAlign.Left;
					if(ck.ClientID.ToLower().EndsWith("e"))
					{
						ck.Text="ENABLE3D";
					}
					break;
					

				case "System.Web.UI.WebControls.DropDownList":
					DropDownList dl;				
					dl=(DropDownList)o;
					dl.CssClass="btnhov";
					dl.Width=200;
					dl.Height=25;
					break;

				case "System.Web.UI.WebControls.DataGrid":
                    DataGrid dg;
					dg=(DataGrid)o;
					dg.CssClass="grid";
//					dg.BackImageUrl="pic2.bmp";
					dg.HeaderStyle.CssClass="ptitle";
					dg.Width=1000;
					
					break;
				case "System.Web.UI.WebControls.Label":
					Label lb;
					lb=(Label)o;
					lb.CssClass="ParamTblTitle";
					lb.Width=100;
					lb.Height=25;
					if(lb.ClientID.ToLower().EndsWith("s"))
					{
						lb.Text="SHOP";
					}
					else if(lb.ClientID.ToLower().EndsWith("l"))
					{
					
						lb.Text="LOT_TYPE";
					}
					else if(lb.ClientID.ToLower().EndsWith("g"))
					{
					
						lb.Text="GLASS_TYPE";
					}
					else if(lb.ClientID.ToLower().EndsWith("sd"))
					{
					
						lb.Text="START_DTTM";
					}
					else if(lb.ClientID.ToLower().EndsWith("ed"))
					{
					
						lb.Text="END_DTTM";
					}
					else if(lb.ClientID.ToLower().EndsWith("vb"))
					{
					
						lb.Text="VIEW_BY";
					}
					else if(lb.ClientID.ToLower().EndsWith("sf"))
					{
					
						lb.Text="SHIFT_DATE";
					}
					else if(lb.ClientID.ToLower().EndsWith("pv"))
					{
					
						lb.Text="PREVIEW";
						lb.ToolTip="當滑鼠移至圖形時可預覽的資料種類";
					}
					else if(lb.ClientID.ToLower().EndsWith("dv"))
					{
					
						lb.Text="DRILLDOWN VIEW";
						lb.ToolTip="當點選圖形時DRILLDOWN呈現的資料";
					}
					else if(lb.ClientID.ToLower().EndsWith("t"))
					{
					
						//lb.Text="TITLE:"+lb.Text;
						lb.CssClass="ParamTitle";
						lb.Width=940;
						lb.Height=50;
					}
					else
					{
						
						
					}
					break;

				default:
				
					break;
			
			
			
			
			}
		
		}
/// <summary>
		/// button 
		/// s:save
		/// c:cancel
		/// e:edit
		/// q:query
		/// cl:clean
		/// m:memory
		/// Label
		/// s:shop
		/// l:lottype
		/// g:glasstype
		/// sd:start_dttm
		/// ed:end_dttm
		/// sf:SHIFT_DATE
		/// vb:view by 
		/// pv:PREVIEW
		/// dv:DRILLDOWN VIEW
		/// t:Report_Name
		/// TextBox:set style
		/// DropDownList:set style
		/// DataGrid
		/// CheckBox:set style 
		/// e:ENABLE3D
/// </summary>
/// <param name="al"></param>

		public void Set_style(ArrayList al)
		{
			Object o;
			for(int i=0;i<al.Count;i++)
			{
			o=al[i];
				switch(o.ToString())
				{
					case "System.Web.UI.WebControls.Button":
						Button bt;
						bt=(Button)o;
									
						if(bt.ClientID.ToLower().EndsWith("s"))
						{
							bt.Text="SAVE";
						}
						else if(bt.ClientID.ToLower().EndsWith("c"))
						{
					
							bt.Text="CANCEL";
						}
						else if(bt.ClientID.ToLower().EndsWith("e"))
						{
					
							bt.Text="EDIT";
						}
						else if(bt.ClientID.ToLower().EndsWith("q"))
						{
					
							bt.Text="QUERY";
						}
						else if(bt.ClientID.ToLower().EndsWith("cl"))
						{
					
							bt.Text="CLEAN";
							bt.ToolTip="清除個人儲存記憶選項";
						}
						else if(bt.ClientID.ToLower().EndsWith("m"))
						{
					
							bt.Text="MEMORY";
							bt.ToolTip="記憶偏好選項,作為每次登入預設選則條件";
						}
						else
						{
						
						
						}

						bt.CssClass="btnhov";
						bt.Width=100;
						bt.Height=25;

						break;

					case "System.Web.UI.WebControls.TextBox":
						TextBox tx;
				
						tx=(TextBox)o;
						tx.CssClass="XPSilverText";
						tx.Width=150;
						tx.Height=25;

						break;
					case "System.Web.UI.WebControls.ListBox":
						ListBox lx;
				
						lx=(ListBox)o;
						lx.CssClass="XPSilverText";
						lx.Width=150;
						lx.Height=75;
					
						if(lx.ClientID.ToLower().EndsWith("s"))
						{
							lx.SelectionMode=ListSelectionMode.Single;
						}
						else
						{
							lx.SelectionMode=ListSelectionMode.Multiple;
					
						}


						break;
					case "System.Web.UI.WebControls.DropDownList":
						DropDownList dl;				
						dl=(DropDownList)o;
						dl.CssClass="btnhov";
						dl.Width=200;
						dl.Height=25;
						break;

					case "System.Web.UI.WebControls.DataGrid":
						DataGrid dg;
						dg=(DataGrid)o;
						dg.CssClass="grid";
						//					dg.BackImageUrl="pic2.bmp";
						dg.Width=1000;
					
						break;
					case "System.Web.UI.WebControls.CheckBox":
						CheckBox ck;
						ck=(CheckBox)o;
						ck.CssClass="ParamTblTitle";
						ck.Width=100;
						ck.Height=25;
						ck.TextAlign=TextAlign.Left;
						if(ck.ClientID.ToLower().EndsWith("e"))
						{
							ck.Text="ENABLE3D";
						}
						break;

						
					case "System.Web.UI.WebControls.Label":
						Label lb;
						lb=(Label)o;
						lb.CssClass="ParamTblTitle";
						lb.Width=100;
						lb.Height=25;
						if(lb.ClientID.ToLower().EndsWith("s"))
						{
							lb.Text="SHOP";
						}
						else if(lb.ClientID.ToLower().EndsWith("l"))
						{
					
							lb.Text="LOT_TYPE";
						}
						else if(lb.ClientID.ToLower().EndsWith("g"))
						{
					
							lb.Text="GLASS_TYPE";
						}
						else if(lb.ClientID.ToLower().EndsWith("sd"))
						{
					
							lb.Text="START_DTTM";
						}
						else if(lb.ClientID.ToLower().EndsWith("ed"))
						{
					
							lb.Text="END_DTTM";
						}
						else if(lb.ClientID.ToLower().EndsWith("vb"))
						{
					
							lb.Text="VIEW_BY";
						}
						else if(lb.ClientID.ToLower().EndsWith("sf"))
						{
					
							lb.Text="SHIFT_DATE";
						}
						else if(lb.ClientID.ToLower().EndsWith("pv"))
						{
					
							lb.Text="PREVIEW";
							lb.ToolTip="當滑鼠移至圖形時預覽的資料";
						}
						else if(lb.ClientID.ToLower().EndsWith("dv"))
						{
					
							lb.Text="DRILLDOWN VIEW";
							lb.ToolTip="當點選圖形時DRILLDOWN呈現的資料";
						}
						else if(lb.ClientID.ToLower().EndsWith("t"))
						{
					
							//lb.Text="TITLE:"+lb.Text;
							lb.CssClass="ParamTitle";
							lb.Width=940;
							lb.Height=50;
							
						}
						else
						{
					
							
						}
						break;

					default:
				
						break;
			
			
			
			
				}
			}
		
		}



	}
}
