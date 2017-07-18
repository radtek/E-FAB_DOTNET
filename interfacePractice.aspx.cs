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

public partial class interfacePractice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SampleClass sc = new SampleClass();
        IControl ctrl = (IControl)sc;
        ISurface srfc = (ISurface)sc;

        // The following lines all call the same method.
        sc.Paint();
        ctrl.Paint();
        srfc.Paint();
        
        sc.Fly();
        ctrl.Eat();
        sc.Eat();
        sc.Hit();
        
      
    }

    public class SampleClass : IControl, ISurface
    {
        void IControl.Paint()
        {
            // System.Console.WriteLine("IControl.Paint");

            System.Web.HttpContext.Current.Response.Write("IControl.Paint</BR>");
        }
        void ISurface.Paint()
        {
            //System.Console.WriteLine("ISurface.Paint");

            System.Web.HttpContext.Current.Response.Write("ISurface.Paint</BR>");
        }

        public void Paint()
        {
            //Console.WriteLine("Paint method in SampleClass");

            System.Web.HttpContext.Current.Response.Write("Paint method in SampleClass</BR>");
        }

        public void Fly()
        {
            //Console.WriteLine("Paint method in SampleClass");

            System.Web.HttpContext.Current.Response.Write("Fly method in SampleClass</BR>");
        }

        public void Eat()
        {
            //Console.WriteLine("Paint method in SampleClass");

            System.Web.HttpContext.Current.Response.Write("Eat method in SampleClass</BR>");
        }


        public void Hit()
        {
            //Console.WriteLine("Paint method in SampleClass");

            System.Web.HttpContext.Current.Response.Write("Hit method in SampleClass</BR>");
        }

        public void Addition()
        {
            //Console.WriteLine("Paint method in SampleClass");

            System.Web.HttpContext.Current.Response.Write("Addition method in SampleClass</BR>");
        }
    }

    interface IControl
    {
        void Paint();
        void Fly();
        void Eat();
        void Hit();
        void Addition();
       // DataTable dt = new DataTable();
        
    }
    interface ISurface
    {
        void Paint();
        void Eat();
        void Hit();
    }
   

}
