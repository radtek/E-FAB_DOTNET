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
using System.Diagnostics;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PerformanceCounter myMemory = new PerformanceCounter();
        PerformanceCounter myCPU = new PerformanceCounter();
        
        myMemory.CategoryName = "Memory";
        myMemory.CounterName = "Available KBytes";
        string txtResult = "-->伺服器可用記憶體大小：" +
        myMemory.NextValue().ToString() + "KB";

       
        myCPU.CategoryName = "Processor";
        myCPU.CounterName = "% Processor Time";

        myCPU.InstanceName = "_Total";
        Response.Write(DateTime.Now.ToLongTimeString() + txtResult+"<br>"+"CPU "+myCPU.NextValue().ToString()+"%");



    }
}
