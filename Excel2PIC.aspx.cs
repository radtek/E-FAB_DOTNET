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
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Drawing;

using xl = Microsoft.Office.Interop.Excel; 






public partial class Excel2PIC : System.Web.UI.Page
{



    //// 關閉Excel Message 
    // Microsoft.Office.Interop.Excel.Application.DisplayAlerts = false; 
    // Microsoft.Office.Interop.Excel.Application.Visible = false; // 設為true,則一開始就會顯現Excel檔. 



    //   //ExlBook = ExlApp.Workbooks.Add(Server.MapPath(".") + "\\Tool_Prod_sample.xls"); 
    //    ExlBook = ExlApp.Workbooks.Add("c:\TAIWAN_BANK_OutSite_Salary_FA_20160111.xls"); 



    protected void Page_Load(object sender, EventArgs e)
    {
        // xl.Application ExlApp ;
        // ExlApp = new xl.ApplicationClass();
        // object missValue = System.Reflection.Missing.Value;

        // string path=@"c:\\TAIWAN_BANK_OutSite_Salary_FA_20160111.xls";
        // Workbook w = ExlApp.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //// (path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        // Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)w.Sheets["Sheet1"];
        // //ws.Protect(Contents: false);
        // Range r = ws.get_Range("B2","H20");

        // r.CopyPicture(XlPictureAppearance.xlScreen, XlCopyPictureFormat.xlBitmap);


        // Bitmap image = new Bitmap(Clipboard.GetImage());
        // image.Save(@"C:\abc\image.png");
        xl.Application xlApp;
        xl.Workbook xlWorkBook;
        xl.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        xlApp = new xl.Application();
        string path=@"c:\\abcd.xls";
        xlWorkBook = xlApp.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        //xlWorkBook = xlApp.Workbooks.Add(misValue);



        xlWorkSheet = (xl.Worksheet)xlWorkBook.Worksheets.get_Item(1);

        xl.Range xlRange;

        xlRange = xlWorkSheet.get_Range("A1", "d5");
        xlRange.CopyPicture(Microsoft.Office.Interop.Excel.XlPictureAppearance.xlScreen, Microsoft.Office.Interop.Excel.XlCopyPictureFormat.xlPicture);
        

        xl.ChartObjects xlCharts = (xl.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
        xl.ChartObject myChart = (xl.ChartObject)xlCharts.Add(10, 80, 300, 250);
        xl.Chart chartPage = myChart.Chart;
        xl.Range chartRange;
        chartRange = xlWorkSheet.get_Range("A1", "d5");
        chartPage.SetSourceData(chartRange, misValue);
        chartPage.ChartType = xl.XlChartType.xlColumnClustered;

        //export chart as picture file
        //chartPage.Paste();

        chartPage.Export(@"C:\excel_chart_export.JPG", "JPG", misValue);
        xlCharts.Delete();
        //xlWorkBook.SaveAs(@"C:\excel_chart_export.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        xlWorkBook.Close(true, misValue, misValue);
        xlApp.Quit();

        releaseObject(xlWorkSheet);
        releaseObject(xlWorkBook);
        releaseObject(xlApp);

    }

    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
        }
        finally
        {
            GC.Collect();
        }
    }


}