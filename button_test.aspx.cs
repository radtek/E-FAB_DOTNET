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

public partial class button_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string AAA = "";
        string tmp_str = "";
        Int32 bbb = 10;
        for (int i = 0; i <= 9; i++)
        {

            if (i < 4)
            {
                AAA = AAA + "*";

                Response.Write(AAA + "<br>");
            }
            else
            {
                for (int j = 0; j <= 8-i; j++)
                {
                    tmp_str = tmp_str + "*";
                }

                Response.Write(tmp_str + "<br>");
                tmp_str = "";
            }

           



        }
     


    }
}
