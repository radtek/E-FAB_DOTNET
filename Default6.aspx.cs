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

public partial class Default6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("AAA");
    }

    public class DrawingObject
    {
        public virtual void Draw()
        {
            Console.WriteLine("I'm just a generic drawing object.");
        }
    }



    public class Line : DrawingObject
    {
        public override void Draw()
        {
            Console.WriteLine("I'm a Line.");

            Response.Write("I AM LINE");

        }
    }

    public class Circle : DrawingObject
    {
        public override void Draw()
        {
            Response.Write("I AM Circle");

        }
    }

    public class Square : DrawingObject
    {
        public override void Draw()
        {
            Response.Write("I AM Square");

        }
    }
    public class Movie
    {
        public int CHILDRENS = 2;
        public int REGULAR = 0;
        public int NEW_RELEASE = 1;
        private String _title;
        private int _priceCode;
        public Movie(String title, int priceCode)
        {
            _title = title;
            _priceCode = priceCode;
        }

        public Movie()
        {
           
        }
        public int getPriceCode()
        {
            return _priceCode;
        }
        public void setPriceCode(int arg)
        {
            _priceCode = arg;
        }
        public String getTitle()
        {
            return _title;
        }
    }

}
