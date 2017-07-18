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

public partial class invoke_test : System.Web.UI.Page
{
    public delegate void SomeAction(string message);
    Int32 abc = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        SomeAction action = ShowMessage1;
        action += ShowMessage2;
        action += ShowMessage3;
        action += ShowMessage4;
        action += ShowMessage5;
        action.Invoke("-");

        Response.Write("************R1*************<br>");

        //實體化一個委派物件,並將符合委派方法簽章的ShowMessage1方法放到委派物件中
        SomeAction action2 = new SomeAction(ShowMessage1);
        action2 += ShowMessage2;
        action2 += ShowMessage3;
        action2 += ShowMessage4;
        action2 += ShowMessage5;
        action2("-");

        Response.Write("************R2*************<br>");
        //不須宣告委派型別,Action就是一個委派型別
        Action<string> action3 = ShowMessage5;
        action3 += ShowMessage4;
        action3 += ShowMessage3;
        action3 += ShowMessage2;
        action3 += ShowMessage1;
        action3("-");

        Response.Write("************R3*************<br>");



    }

    public  void ShowMessage1(string message)
    {
        //for (int i = 0; i < 1000; i++)
        //{
            
        //}
        Response.Write("大" + message);
    }

    public  void ShowMessage2(string message)
    {
        Response.Write("牛" + message);
    }

    public  void ShowMessage3(string message)
    {
        Response.Write("比" + message);
    }

    public  void ShowMessage4(string message)
    {
        Response.Write("較" + message);
    }

    public  void ShowMessage5(string message)
    {
        Response.Write("懶" + message);
    }
}
