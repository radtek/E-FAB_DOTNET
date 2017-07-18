using System;
using Dundas.Charting.WebControl;
using System.Data;
using System.Collections;
using System.Drawing;


namespace Innolux.Portal.CommonFunction
{
	/// <summary>
	/// chart 的摘要描述。
	/// </summary>
	public class chart
	{
		public chart()
		{
			//
			// TODO: 在此加入建構函式的程式碼
			//
		}
		private void SetChartAttribute(Chart Chart1,ArrayList Axis,bool Enable3D,string title) 
		{
			Chart1.ChartAreas["Default"].AxisX.Interval = 1;
			
			if(Axis.Count>=1)
			{
				Chart1.ChartAreas["Default"].AxisX.Title  = Axis[0].ToString();
			}
			if(Axis.Count>=2)
			{
				Chart1.ChartAreas["Default"].AxisY.Title = Axis[1].ToString();
			}
			if(Axis.Count>=3)
			{
			Chart1.ChartAreas["Default"].AxisY2.Title = Axis[2].ToString();
			Chart1.ChartAreas["Default"].AxisY2.LineColor = Color.SlateBlue;
   
				// Set Axis Line Style
			Chart1.ChartAreas["Default"].AxisY2.LineStyle = ChartDashStyle.DashDot;
   
				// Set Arrow Style
			Chart1.ChartAreas["Default"].AxisY2.Arrows = ArrowsType.SharpTriangle;
   
				// Set Line Width
			Chart1.ChartAreas["Default"].AxisY2.LineWidth = 1;
			
			
			}
			
			
			
			// Set legend 
			
			Chart1.Legends["Default"].LegendStyle = LegendStyle.Row;
			Chart1.Legends["Default"].Docking = LegendDocking.Top;
			Chart1.Legends["Default"].Alignment = StringAlignment.Center;
			Chart1.Legends["Default"].BackColor=Color.Transparent;
			Chart1.Legends["Default"].BorderColor=Color.Transparent;
			
			Chart1.Legends["Default"].Title=title;
			Chart1.Legends["Default"].TitleColor=Color.Silver;
			Chart1.Legends["Default"].TitleFont=new Font("Times New Roman", 14, FontStyle.Bold);

			
			// Set ChartAreas

			Chart1.ChartAreas["default"].Area3DStyle.Enable3D=Enable3D;
			Chart1.ChartAreas["default"].BackColor=Color.Transparent;
			Chart1.ChartAreas["default"].BorderStyle=ChartDashStyle.Solid;

			Chart1.ChartAreas["default"].AxisX.LineColor=Color.DimGray;
			Chart1.ChartAreas["default"].AxisX.MajorTickMark.LineColor=Color.DimGray;
			Chart1.ChartAreas["default"].AxisX.LineStyle=ChartDashStyle.Dot;

			Chart1.ChartAreas["default"].AxisY.LineColor=Color.DimGray;
			Chart1.ChartAreas["default"].AxisY.MajorTickMark.LineColor=Color.DimGray;
			Chart1.ChartAreas["default"].AxisY.LineStyle=ChartDashStyle.Dot;

//			Chart1.ChartAreas["default"].AxisX2.LineColor=Color.DimGray;
//			
//			Chart1.ChartAreas["default"].AxisY2.LineColor=Color.DimGray;
		

			
//			Chart1.ImageType=ChartImageType.Flash;
//			Chart1.RepeatAnimation = true;
//             Chart1.RepeatDelay = 1;

			if(Chart1.ID.ToLower().EndsWith("s"))
			{
				Chart1.Width=250;
				Chart1.Height=250;
				//Chart1.ImageUrl=@"~\"+title.Substring(0,6)+"_#SEQ(300,3)";
			}
			else
			{
				Chart1.Width=950;
				Chart1.Height=500;
				
			}
			Chart1.ImageUrl=@"~\tmp_img\"+title.Substring(0,6)+"_#SEQ(300,3)";
			Chart1.BackColor=Color.Azure;
			Chart1.BackGradientType=GradientType.DiagonalLeft;
			Chart1.BackGradientEndColor=Color.SkyBlue;
			Chart1.BorderLineColor=Color.Gray;
			Chart1.Palette=ChartColorPalette.SemiTransparent;
			Chart1.BorderSkin.SkinStyle=BorderSkinStyle.Emboss;
			Chart1.BorderSkin.FrameBackColor=Color.SkyBlue;
			Chart1.BorderSkin.FrameBackGradientEndColor=Color.DodgerBlue;
			Chart1.BorderSkin.PageColor=Color.AliceBlue;

			
		}
		private void Load_tmp(Chart Chart1)
		{
//			HttpServerUtility h=new HttpServerUtility();
//			Chart1.LoadTemplate(h.MapPath("ChartTemplate1.xml"));

		
		}
		private Color strColor(int index)
		{
			switch (index)
			{
				
				case 0 : return Color.Yellow;
				case 1 : return Color.Aqua;
				case 2 : return Color.Beige;
				case 3 : return Color.Honeydew;
				case 4 : return Color.Violet; //Blue Shade
				case 5 : return Color.YellowGreen; //Bright Red
				case 6 : return Color.Blue; //Dark Green
				case 7 : return Color.RoyalBlue; //Blue (Light)
				case 8 : return Color.Salmon; //Dark Pink
				case 9 : return Color.Aquamarine; //Variant of brown
				case 10 : return Color.Thistle; //Dirty green
				case 11 : return Color.Yellow; //Violet shade of blue
				case 12 : return Color.Silver; //Orange
				case 13 : return Color.Purple; //Chrome Yellow+Green
				case 14 : return Color.Salmon; //Violet
				case 15 : return Color.PowderBlue; //Grey
				case 16 : return Color.PeachPuff; //Blue+Green Light
				case 17 : return Color.Navy; //Light violet
				case 18 : return Color.OldLace; //Shade of green
				case 19 : return Color.MintCream; //Dark Blue
				case 20 : return Color.Olive; //Grey
				case 21 : return Color.Orange; //Blue+Green Light
				case 22 : return Color.Orchid; //Light violet
				case 23 : return Color.MistyRose; //Shade of green
				case 24 : return Color.LightSeaGreen; //Dark Blue
	
				default : return Color.Red; 
			}

		}
		private SeriesChartType Get_SeriesChartType(string s)
		{
		
			switch (s)
			{
				case "1":
				return SeriesChartType.StackedColumn;
					
				case "2":
				return SeriesChartType.Column;
					
				case "3":
				return SeriesChartType.Line;
				

				case "4":
				return SeriesChartType.Bar;

				

				case "5":
				return SeriesChartType.Pie;
					

				default:
				return SeriesChartType.StackedColumn;
				
			
			
			
			
			}
		
		
		
		}
		/// <summary>
		/// Axis_value:Display_QTY
		/// Series_ChartType:1 StackedColumn 2 Column 3.Line 4 Bar 5 Pie
		/// Axis: X Y Y2
		/// Enable3D  true   or false
		/// title Chart title
		/// </summary>
		/// <param name="Chart1"></param>
		/// <param name="ds"></param>
		/// <param name="Axis_value"></param>
		/// <param name="Series_ChartType"></param>
		/// <param name="Axis"></param>
		/// <param name="url"></param>
		/// <param name="Enable3D"></param>
		/// 
		public void Do_chart(Chart Chart1,DataSet ds,ArrayList Axis_value,ArrayList Series_ChartType,ArrayList Axis,bool Enable3D,string title)
		{
			SetChartAttribute(Chart1,Axis,Enable3D,title);
			//			Load_tmp(Chart1);
			int pidx=0;
			DataView dv=ds.Tables[0].DefaultView; 
			Chart1.Series.Clear();
			Series s;
			
			for(int i=0;i<Axis_value.Count;i++)
			{

				s=Chart1.Series.Add("Series" + (Chart1.Series.Count+1).ToString());
				s.ChartArea = "Default";
				s.Type =Get_SeriesChartType(Series_ChartType[i].ToString());
				s.Name=Axis_value[i].ToString();
//				s.Color=strColor(i);
				if(i==Axis_value.Count-1)
				{
					if(Axis.Count==3)
					{
						s.YAxisType = AxisType.Secondary;
						s.Color=Color.LightGoldenrodYellow;
						s.BorderWidth=3;
					}
				}
					
			
			}
			
			foreach( DataRow r in ds.Tables[0].Rows)
						
			{

				if (dv.Count>0)
				{

					for(int i=0;i<Chart1.Series.Count;i++)
					{
						
						pidx=Chart1.Series[i].Points.AddXY(r[Axis[0].ToString()], r[Axis_value[i].ToString()]);
						Chart1.Series[i].Points[pidx].ToolTip=String.Format(""+Axis[0].ToString()+" : {0} \n"+Axis_value[i].ToString()+" : {1}",r[Axis[0].ToString()],r[Axis_value[i].ToString()]);


					}
					
				}
			}
		


			



		}			
		/// <summary>
		/// Axis_value:Display_QTY
		/// Series_ChartType:1 StackedColumn 2 Column 3.Line 4 Bar 5 Pie
		/// Axis: X Y Y2
		/// Enable3D  true   or false
		/// title Chart title
		/// </summary>
		/// <param name="Chart1"></param>
		/// <param name="ds"></param>
		/// <param name="Axis_value"></param>
		/// <param name="Series_ChartType"></param>
		/// <param name="Axis"></param>
		/// <param name="url"></param>
		/// <param name="Enable3D"></param>
		public void Do_chart(Chart Chart1,DataSet ds,ArrayList Axis_value,ArrayList Series_ChartType,ArrayList Axis,string url,bool Enable3D,string title)
		{
			SetChartAttribute(Chart1,Axis,Enable3D,title);
//			Load_tmp(Chart1);
			int pidx=0;
			DataView dv=ds.Tables[0].DefaultView; 
			Chart1.Series.Clear();
			Series s;
			
			for(int i=0;i<Axis_value.Count;i++)
			{

				s=Chart1.Series.Add("Series" + (Chart1.Series.Count+1).ToString());
				s.ChartArea = "Default";
				s.Type =Get_SeriesChartType(Series_ChartType[i].ToString());
				s.Name=Axis_value[i].ToString();
//				s.Color=strColor(i);
				if(i==Axis_value.Count-1)
				{
					if(Axis.Count==3)
					{
						s.YAxisType = AxisType.Secondary;
						s.Color=Color.LightGoldenrodYellow;
						s.BorderWidth=3;
					}
				}
					
			
			}
			if(url.IndexOf("?")>0)
			{
				url=url+"&";
			}
			else
			{
				url=url+"?";
			}
			
			foreach( DataRow r in ds.Tables[0].Rows)
						
			{

				if (dv.Count>0)
				{
					
					for(int i=0;i<Chart1.Series.Count;i++)
					{
						pidx=Chart1.Series[i].Points.AddXY(r[Axis[0].ToString()], r[Axis_value[i].ToString()]);
						Chart1.Series[i].Points[pidx].ToolTip=String.Format(""+Axis[0].ToString()+" : {0} \n"+Axis_value[i].ToString()+" : {1}",r[Axis[0].ToString()],r[Axis_value[i].ToString()]);
						Chart1.Series[i].Points[pidx].MapAreaAttributes="TARGET='_blank'";
						Chart1.Series[i].Points[pidx].Href=url+"Axisx="+r[Axis[0].ToString()]+"";

					}
					
				}
			}
		


			



		}			
		/// <summary>
		/// Axis_value:Display_QTY
		/// Series_ChartType:1 StackedColumn 2 Column 3.Line 4 Bar 5 Pie
		/// Axis: X Y Y2
		/// Enable3D  true   or false 
		/// title Chart title
		/// </summary>
		/// <param name="Chart1"></param>
		/// <param name="ds"></param>
		/// <param name="Axis_value"></param>
		/// <param name="Series_ChartType"></param>
		/// <param name="Axis"></param>
		/// <param name="url"></param>
		/// <param name="tooltip_path"></param>
		/// <param name="Enable3D"></param>
		public void Do_chart(Chart Chart1,DataSet ds,ArrayList Axis_value,ArrayList Series_ChartType,ArrayList Axis,string url,string tooltip_path,bool Enable3D,string title)
		{
			SetChartAttribute(Chart1,Axis,Enable3D,title);
			//			Load_tmp(Chart1);
			int pidx=0;
			DataView dv=ds.Tables[0].DefaultView; 
			Chart1.Series.Clear();
			Series s;
			
			for(int i=0;i<Axis_value.Count;i++)
			{

				s=Chart1.Series.Add("Series" + (Chart1.Series.Count+1).ToString());
				s.ChartArea = "Default";
				s.Type =Get_SeriesChartType(Series_ChartType[i].ToString());
				s.Name=Axis_value[i].ToString();
				//				s.Color=strColor(i);
				if(i==Axis_value.Count-1)
				{
					if(Axis.Count==3)
					{
						s.YAxisType = AxisType.Secondary;
						s.Color=Color.LightGoldenrodYellow;
						s.BorderWidth=3;
					}
				}
					
			
			}
			if(tooltip_path.IndexOf("?")>0)
			{
				tooltip_path=tooltip_path+"&";
			}
			else
			{
				tooltip_path=tooltip_path+"?";
			}
			if(url.IndexOf("?")>0)
			{
				url=url+"&";
			}
			else
			{
				url=url+"?";
			}


			foreach( DataRow r in ds.Tables[0].Rows)
						
			{

				if (dv.Count>0)
				{

					for(int i=0;i<Chart1.Series.Count;i++)
					{
						string toolTip = "";
						toolTip = "<IMG SRC="+tooltip_path+"Axisx="+r[Axis[0].ToString()]+">";
//						toolTip = "<IMG SRC="+tooltip_path+">";
						pidx=Chart1.Series[i].Points.AddXY(r[Axis[0].ToString()], r[Axis_value[i].ToString()]);
						Chart1.Series[i].Points[pidx].ToolTip=String.Format(""+Axis[0].ToString()+" : {0} \n"+Axis_value[i].ToString()+" : {1}",r[Axis[0].ToString()],r[Axis_value[i].ToString()]);
						//						Chart1.Series[i].Points[pidx].MapAreaAttributes="TARGET='_blank'";
						Chart1.Series[i].Points[pidx].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip + "');\" onmouseout=\"DisplayTooltip('');\" TARGET='_blank'";
						Chart1.Series[i].Points[pidx].Href=url+"Axisx="+r[Axis[0].ToString()]+"";	

					}
					
				}
			}
		


			



		}			
	}
}
