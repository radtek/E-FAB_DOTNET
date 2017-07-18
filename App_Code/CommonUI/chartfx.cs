using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using ChartFX.WebForms;
using ChartFX.WebForms.Galleries;
using ChartFX.WebForms.Adornments;

namespace Innolux.Portal.CommonFunction
{
    public class chartfx
    {
        public chartfx()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void setchartfx(DataTable dt, ChartFX.WebForms.Chart chart, string title)
        {
            TitleDockable newTitle = new TitleDockable();
            chart.DataSource = dt;
            chart.DataBind();
            chart.Zoom = true;
            newTitle.Text = title;
            chart.Titles.Add(newTitle);
            newTitle.Font = new Font(newTitle.Font, FontStyle.Bold);
            newTitle.Dock = DockArea.Top;
            chart.LegendBox.Visible = false;
            chart.Width = 300;
            chart.Height = 200;
            chart.RenderFormat = "Image";
            //Link to Chart
            Link seriesLink = chart.Series[0].Link;
            seriesLink.OnMouseOver = "DisplayTooltip('<IMG SRC=../ChartReport_Detail.aspx>');";
            seriesLink.OnMouseOut = "DisplayTooltip('');";
        }
        public void setchartfxStyle(ChartFX.WebForms.Chart FX, ChartFX.WebForms.Gallery ChartGallery, string title, int AxisX_Step, int X_ScaleUnit, int Y_ScaleUnit, bool showTargetLine, int TargetQty)
        {

            //FX.Height = Unit.Pixel(180);
            //FX.Width = Unit.Pixel(210);
            //FX.EnableViewState = false;
            FX.Gallery = ChartGallery;
            FX.AllSeries.FillMode = FillMode.Gradient;
            GradientBackground myGradient = new GradientBackground(GradientType.Radial);
            //myGradient.ColorFrom = Color.Black;
            //myGradient.ColorTo = Color.Black;
            myGradient.Angle = 45;
            SimpleBorder myBorder = new SimpleBorder(SimpleBorderType.Etched);
            //myBorder.Color = Color.Gray;
            myBorder.InternalOpposite = false;
            FX.Background = myGradient;
            FX.Border = myBorder;
            FX.LegendBox.Visible = false;
            FX.RenderFormat = "Image";
            //FX.AllSeries.Border.Color = Color.Black;
            TitleDockable oTitle = new TitleDockable();
            oTitle.Text = title;
            oTitle.TextColor = Color.Red;
            FX.Titles.Add(oTitle);

            FX.AxisX.ForceZero = true;
            FX.AxisX.ScrollPosition = 200;
            FX.AxisY2.Grids.Major.Visible = false;
            FX.AxisX.Grids.Major.Visible = false;
            //FX.ForeColor = Color.Yellow;
            //FX.ChartAreaRectangle.Location.
            FX.PlotAreaMargin.Top = 20;
            FX.PlotAreaMargin.Left = 10;
            FX.PlotAreaMargin.Bottom = 10;
            FX.PlotAreaMargin.Right = 10;
            if (AxisX_Step > 0)
            {
                FX.AxisX.Step = double.Parse(AxisX_Step.ToString());
            }
            if (showTargetLine == true)
            {
                ShowTagLine_FX(FX, TargetQty);
            }
            if (X_ScaleUnit > 0)
            {
                FX.AxisX.ScaleUnit = double.Parse(X_ScaleUnit.ToString());
            }
            if (Y_ScaleUnit > 0)
            {
                FX.AxisY.ScaleUnit = double.Parse(Y_ScaleUnit.ToString());
            }
            FX.ToolBar.Visible = false;
           




        }
        public void setchartfxStyle(ChartFX.WebForms.Chart FX, ChartFX.WebForms.Gallery ChartGallery, string title, int AxisX_Step, int X_ScaleUnit, int Y_ScaleUnit, bool showTargetLine, int TargetQty,bool b)
        {

            
            FX.Gallery = ChartGallery;
            FX.AllSeries.FillMode = FillMode.Gradient;
            GradientBackground myGradient = new GradientBackground(GradientType.BackwardDiagonal);
            myGradient.ColorFrom = Color.Transparent;
            myGradient.ColorTo = Color.Transparent;
            myGradient.Angle = 45;
            SimpleBorder myBorder = new SimpleBorder(SimpleBorderType.Color);
            myBorder.Color = Color.Gray;
            myBorder.InternalOpposite = false;
            FX.Background = myGradient;
            FX.Border = myBorder;
            FX.LegendBox.Visible = false;
            FX.RenderFormat = "Image";
            FX.AllSeries.Border.Color = Color.Black;
            TitleDockable oTitle = new TitleDockable();
            oTitle.Text = title;
            oTitle.TextColor = Color.Red;
            FX.Titles.Add(oTitle);
            FX.AxisX.Grids.Major.Visible = false;
            FX.AxisY.Grids.Major.Visible = false;

            FX.AxisX.ForceZero = true;
            FX.AxisX.ScrollPosition = 200;
            FX.ForeColor = Color.Yellow;
            //FX.ChartAreaRectangle.Location.
            FX.PlotAreaMargin.Top = 20;
            FX.PlotAreaMargin.Left = 10;
            FX.PlotAreaMargin.Bottom = 10;
            FX.PlotAreaMargin.Right = 10;
            if (AxisX_Step > 0)
            {
                FX.AxisX.Step = double.Parse(AxisX_Step.ToString());
            }
            if (showTargetLine == true)
            {
                ShowTagLine_FX(FX, TargetQty);
            }
            if (X_ScaleUnit > 0)
            {
                FX.AxisX.ScaleUnit = double.Parse(X_ScaleUnit.ToString());
            }
            if (Y_ScaleUnit > 0)
            {
                FX.AxisY.ScaleUnit = double.Parse(Y_ScaleUnit.ToString());
            }





        }
        public void ShowTagLine_FX(ChartFX.WebForms.Chart ChartName, int TagValue)
        {

            CustomGridLine TagLine = new CustomGridLine();
            TagLine.Color = Color.Red;
            TagLine.Value = TagValue;
            TagLine.ShowLine = true;
            TagLine.ShowText = true;
            TagLine.TextColor = Color.Red;
            ChartName.AxisY.CustomGridLines.Add(TagLine);
            ConditionalHighlightAttributes hCustomGridAttr = ChartName.AxisY.CustomGridLines[0].Highlight;
            hCustomGridAttr.Series = 1;
        }

    }
}