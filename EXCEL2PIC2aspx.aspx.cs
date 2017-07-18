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
using Excel1 = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Text;


public partial class EXCEL2PIC2aspx : System.Web.UI.Page
{
    Excel1.Application _Excel = null;

    string today_yyyymmdd = DateTime.Now.AddDays(+0).ToString("yyyyMMdd");
    string today_yyyymmddHH = DateTime.Now.AddDays(+0).ToString("yyyyMMddHH");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");
    string today_HH = DateTime.Now.AddDays(+0).ToString("HH");
    string tool = "";
    string website = "";
    string title = "";
    string strHTML = "";
    string mail_list = "";
    
   
    protected void Page_Load(object sender, EventArgs e)
    {
       // System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;

        Thread myth;
        myth = new Thread(new System.Threading.ThreadStart(Test1));
        myth.ApartmentState = ApartmentState.STA;
        myth.Start();


       // Test1();
    }
    public void Test1()
    {
        /* Excel.Application：Excel應用程式
         Excel.Workbook：應用程式裡的活頁簿，預設情況下，不管你開幾個Excel檔案，在工作管理員裡只會出現一個Excel.exe
         Excel.Worksheet：活頁簿裡的工作表
         Excel.Range：工作表裡的儲存格，一格也是Range，多格也是Range，用法Excel.Range[“A1”];
         Excel.Range.Cells：這是儲存格的最小單位，代表一格的Range，用法Excel.Range.Cells[1,1];*/

        initailExcel();


        openExcel();

    }

    public void initailExcel()
    {
        //檢查PC有無Excel在執行
        bool flag = false;

   
        foreach ( Process clsProcess in Process.GetProcesses())
        {
            if (clsProcess.ProcessName == "EXCEL")
            {
                flag = true;
                break;
            }
        }

        if (!flag)
        {
            this._Excel = new Excel1.Application();
        }
        else
        {
            object obj = Marshal.GetActiveObject("Excel.Application");//引用已在執行的Excel
            _Excel = obj as Excel1.Application;
        }

        this._Excel.Visible = true;//設false效能會比較好
    }



   public void openExcel()
    {
        Excel1.Workbook book = null;
        Excel1.Worksheet sheet = null;


        string MapPath = Server.MapPath(".");
        //string path = MapPath + @"\T2Cell_Noon_20150722171337.xls";
        string path = @"c:\\TAIWAN_BANK_OutSite_Salary_FA_20160111.xls";
        try
        {
            book = this.ExcelWorkbookOpen(path);//開啟Excel檔


            book.Save();

            string QQ = _Excel.Version.ToString();




            sheet = (Excel1.Worksheet)book.Sheets[1];//轉換的Sheet


            System.Drawing.Image a = this.SheetToImage(sheet, "A11", "L88");//Sheet轉圖檔
            a.Save(MapPath+"..\\File\\" + today_detail + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//儲存圖檔
            //a.Save(MapPath + @"\20160112_QOO.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);//儲存圖檔


        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());

        }
        finally
        {

            book = null;
            sheet = null;


        }


    }

    /// <summary>
    /// 將Excel的Sheet轉成Image
    /// </summary>
    /// <param name="sheet">工作表</param>
    /// <param name="inFrom">起始位子</param>
    /// <param name="inTo">結束位子</param>
    /// <returns></returns>
    private System.Drawing.Image SheetToImage(Excel1.Worksheet sheet, string inFrom, string inTo)
    {
        Excel1.Range range = null;
        System.Drawing.Image ToImage = null;

        try
        {

            range = sheet.get_Range(inFrom, inTo);

            range.CopyPicture(Excel1.XlPictureAppearance.xlScreen, Excel1.XlCopyPictureFormat.xlBitmap);

            ToImage = System.Windows.Forms.Clipboard.GetImage();


            _Excel = null;

        }
        catch (Exception ex)
        {
            //throw new System.Exception(ex.Message.ToString());

        }
        finally
        {
            System.Windows.Forms.Clipboard.Clear();
        }

        return ToImage;
    }



    /// <summary>
    /// 開啟Excel
    /// </summary>
    /// <param name="inPath">路徑</param>
    /// <returns></returns>
    private Excel1.Workbook ExcelWorkbookOpen(string inPath)
    {

        return _Excel.Workbooks.Open(inPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
