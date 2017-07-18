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

public partial class lottory : System.Web.UI.Page
{
    Int32 choose_area = 0;

    Int32 choose_num = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            TextBox1.Text = "42";
            TextBox2.Text = "6";
        
        
        
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        choose_area = Convert.ToInt32(TextBox1.Text);
        choose_num = Convert.ToInt32(TextBox2.Text);

            string s = "";
            int[] loto = new int[choose_area]; //將所有號碼(1~42)放入陣列loto中   
            for (int i = 0; i <= choose_area-1; i++)
            {
                loto[i] = i + 1;
            }
            int[] x = new int[choose_num+1]; //宣告要取多少個數字   
            Random r = new Random();
            for (int j = 0; j <= choose_num-1; j++)
            {
                int temp = r.Next(1, choose_area); //隨機抓取一組數字放入x[]陣列中   
                if (loto[temp] == 0) { j--; }//如果數字為0，則重新產生亂數   
                else
                {
                    x[j] = loto[temp]; //否則將亂數產生之數字放入x[]陣列中   
                    s += x[j].ToString() + ",";//每個數字以,分隔   
                    loto[temp] = 0; //將以使用之數字以零取代   
                    TextBox3.Text = s.Substring(0, s.Length - 1);//去除最後一個逗號   
                    if (j == choose_num)
                    {
                        Response.Write("Happy New Year:D");
                    }
                }
            }
    } 
    
}
