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

public partial class Query_class : System.Web.UI.Page
{
    ArrayList arlist_temp1 = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {

       if(!IsPostBack)
       {
           txtEstimateStartDate.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd"));
           arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\hour.txt");

           arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\hour.txt");

           DropDownList5.DataSource = arlist_temp1;
           DropDownList5.DataBind();
           DropDownList5.Text = DateTime.Now.ToString("HH");


           arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

           DropDownList8.DataSource = arlist_temp1;
           DropDownList8.DataBind();
           DropDownList8.Text = DateTime.Now.ToString("mm");
       
       }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string baseDate = "2010/01/02";

        int count = 0;

        System.DateTime time1 = Convert.ToDateTime(baseDate);
        System.DateTime time2 = Convert.ToDateTime(txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd"));


        TimeSpan ts = time2.Subtract(time1);
        Int32 diffm = Convert.ToInt32(ts.TotalDays);
        Int32 diffm2=diffm % 4;
        string class_AB= "";
        string class_DN = "";
        string combine = "";

        if (diffm2.Equals(0) )

        {
            class_AB = "A(1)";
        }
        else if (diffm2.Equals(1))
        {

            class_AB = "A(2)";
        }
        else if (diffm2.Equals(2))
        {
            class_AB = "B(1)";
        }
        else
        {

            class_AB = "B(2)";
        
        }

        if (Convert.ToInt32(DropDownList5.SelectedValue) > 7 && Convert.ToInt32(DropDownList5.SelectedValue) < 19)
        {
            class_DN = "D";

        }
        else

        {
            class_DN = "N";
        
        }
      


        combine = class_DN + class_AB;

        Label1.Visible = true;

        Label1.Text = combine;



    }
}
