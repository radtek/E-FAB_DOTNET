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
using System.ComponentModel;
using System.Threading;

public partial class Alarm_thread_sample : System.Web.UI.Page
{
    private BackgroundWorker bw = new BackgroundWorker();

    string Stemp = "";
    protected void Page_Load(object sender, EventArgs e)
    {



    }



    public void Simplest()
    {
        Stemp = Stemp + "Oscar";






    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        for (int i = 0; i <= 1; i++)
        {
            ThreadStart simplest = new ThreadStart(Simplest);
            Thread thread1 = new Thread(simplest);
            thread1.Start();

            Thread.Sleep(300000);
        }


        TextBox1.Text = Stemp;


    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }


}
