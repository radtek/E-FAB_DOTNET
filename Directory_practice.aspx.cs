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
using System.IO;

public partial class Directory_practice : System.Web.UI.Page
{
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    protected void Page_Load(object sender, EventArgs e)
    {


        DirectoryInfo dir = new DirectoryInfo(Server.MapPath(".") + "\\CF\\Save_file\\");
        FileInfo[] files = dir.GetFiles("*.xls");
                

        for (int i = 0; i <= files.Length-1; i++)
        {
            if (files[i].CreationTime < DateTime.Now.AddDays(-30))
            {

                files[i].Delete();
            }
            
           

        }

        Int32 counter = 0;
        // Display the name of all the files.
        foreach (FileInfo file in files)
        {
           counter=counter+1;
            Response.Write(counter+".");
            
            Response.Write("Name: " + file.Name + "  ");
            Response.Write("<br/>");
            Response.Write("Size: " + file.Length.ToString());
            Response.Write("<br/>");
        }


    }
}
