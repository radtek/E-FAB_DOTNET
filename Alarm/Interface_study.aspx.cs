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

public partial class Alarm_Interface_study : System.Web.UI.Page
{
   public static string aabbcc = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        IDisplayCard displayCard = new AsusDisplayCard();
        IDisplayCard displayCard1 = new GigaDisplayCard();

        LooseCouplingMotherBoard looseCoupling = new LooseCouplingMotherBoard();

        looseCoupling.Display("我是寬鬆耦合主機板程式", displayCard);

        Response.Write(aabbcc+"<BR>");

        looseCoupling.Display("我是寬鬆耦合主機板程式", displayCard1);

        Response.Write(aabbcc+"<BR>");
    }

    class LooseCouplingMotherBoard
    {
        public LooseCouplingMotherBoard()
        {
        }

        public void Display(string CPU送來的影像資料, IDisplayCard displayCard)
        {
            displayCard.Display(CPU送來的影像資料);
        }

        public void Cpu(string 運算資料)
        { }

        internal static void Display(string p, object p_2)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }


    interface IDisplayCard
    {
        void Display(string 顯示訊號);
    }


    class AsusDisplayCard : System.Web.UI.Page,IDisplayCard

    {
        public void Display(string CPU送來的影像資料)
        {
         aabbcc = "我是寬鬆耦合主機板的華碩顯示卡" + "將 CPU 送來的資料送到顯卡晶片裏面進行處理" + " Asus" + " Asus" + "*********";

            //Response.Write("我是寬鬆耦合主機板的華碩顯示卡");
            //Response.Write("將 CPU 送來的資料送到顯卡晶片裏面進行處理");
            //Response.Write("將晶片處理完的資料送到顯存");
            //Response.Write("進行資料轉換的工作(數位轉類比)");
            //Response.Write("我有多了圖形加速功能");
            //Response.Write("將轉換完的類比資料送到螢幕");
            //Response.Write("Asus");
            //Response.Write("Asus");
            //Response.Write("*********");
        }
    }




    class GigaDisplayCard : System.Web.UI.Page, IDisplayCard
    {
        public void Display(string CPU送來的影像資料)
        {

        aabbcc = "我是寬鬆耦合主機板的技嘉顯示卡" + "將 CPU 送來的資料送到顯卡晶片裏面進行處理" + " Giga" + " Giga" + "*********";
            //Response.Write("我是寬鬆耦合主機板的技嘉顯示卡");
            //Response.Write("將 CPU 送來的資料送到顯卡晶片裏面進行處理");
            //Response.Write("將晶片處理完的資料送到顯存");
            //Response.Write("進行資料轉換的工作(數位轉類比)");
            //Response.Write("我有多了圖形加速功能");
            //Response.Write("將轉換完的類比資料送到螢幕");
            //Response.Write("Giga");
            //Response.Write("Giga");
            //Response.Write("*********");
        }
    }
}
