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

public partial class AbstractSample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        double Area_Answer=0;
        Circle c1 = new Circle(12);
        Response.Write(Convert.ToString(c1.getArea())+"<br>" );
        RecTangle t1 = new RecTangle(5, 8);
        Response.Write(Convert.ToString(t1.getArea()) + "<br>");
       // Area_Answer = abc.getArea();

    }
   public abstract class Geometry
    {
        public abstract double getArea();

        
        //public abstract double getVolume();
    }
   public class Circle : Geometry
    {
        double r;
      public  Circle(double r)
        {
            this.r = r;
        }
        public override double getArea()
        {
            return (3.14 * r * r);
        }
    }

    public class RecTangle : Geometry
    {
        double a;
        double b;
        public RecTangle(double a, double b)
        {
            this.a = a;
            this.b = b;

        }
        public override double getArea()
        {
            return (a*b);
        }
    }

   public  class  Pillar
   
   {
    Geometry botom;
    Double  height;
    Pillar(Geometry bottom, double height)
    {
        this.bottom = bottom;
        this.height = height;
    }
    public  double  getVolume()
    {
        return  bottom.getArea() * height;
    }
   }

}




}
