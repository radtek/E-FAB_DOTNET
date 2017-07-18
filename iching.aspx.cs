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

public partial class iching : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TextBox1.Text = "";

        if (!IsPostBack)
        {
            TextBox2.Text = "2";
            TextBox3.Text = "6";

            Label3.Text = "";
            Label4.Text = "";

            Button1_Click(null,null);
        
        }

      

       

    }

    public class Divination
    {
        // Field
        public Int32 range;

        public Int32 reapeat;

        public string result;

        public string upState ;
        public string downState;


        // Constructor that takes one argument.
        public Divination(Int32 Irange, Int32 Ireapeat)
        {
            range = Irange;
            reapeat = Ireapeat;
            //AmsChooseStep = choose_step;
            //AMSOwnerID = Ownerid;
        }

        public Divination()
        {
            //range = Irange;
            //reapeat = Ireapeat;
            //AmsChooseStep = choose_step;
            //AMSOwnerID = Ownerid;
        }


        // Method
          public void GetData()
        {

            Random r = new Random();
            result = "";

            for (int i = 0; i <= reapeat-1; i++)
            {

                result += r.Next(range).ToString();
            }
            



        }

        public void GetState()
        {

            result.Substring(0, 3);




        }

        public string GetChineseState(string symbol)
        {
            string stateResult = "";
            string caseSwitch = symbol;
            switch (caseSwitch)
            {
                case "000":
                    stateResult = "坤";
                    break;
                
                case "001":
                    stateResult= "震";
                    break;
                case "010":
                    stateResult= "坎";
                    break;
                case "011":
                    stateResult= "兌";
                    break;

                case "100":
                    stateResult= "艮";
                    break;

                case "101":
                    stateResult= "離";
                    break;

                case "110":
                    stateResult = "巽";
                    break;
                case "111":
                    stateResult= "乾";
                    break;
            }

            return stateResult;
        
        }



        public string GetStateHyperLink(string symbol)
        {
            string header = "http://www.ai5429.com/17/8x/";

            string stateResult = "";

            string rear = ".htm";

            string numberResult = "";

            string caseSwitch = symbol;
            switch (caseSwitch)
            {
                case "乾乾":
                    stateResult = "00001";
                    break;

                case "坤坤":
                    stateResult = "00002";
                    break;
                case "坎雷":
                    stateResult = "00003";
                    break;
                case "艮坎":
                    stateResult = "00004";
                    break;

                case "坎乾":
                    stateResult = "00005";
                    break;

                case "乾坎":
                    stateResult = "00006";
                    break;

                case "坤坎":
                    stateResult = "00007";
                    break;
                case "坎坤":
                    stateResult = "00008";
                    break;

                case "巽乾":
                    stateResult = "00009";
                    break;

                case "天兌":
                    stateResult = "00010";
                    break;

                case "坤乾":
                    stateResult = "00011";
                    break;

                case "乾坤":
                    stateResult = "00012";
                    break;

                case "乾離":
                    stateResult = "00013";
                    break;

                case "離乾":
                    stateResult = "00014";
                    //離乾大有
                    break;

                case "坤艮":
                    stateResult = "00015";
                    break;

                case "震坤":
                    stateResult = "00016";
                    break;

                case "兌震":
                    stateResult = "00017";
                    break;

                case "艮巽":
                    stateResult = "00018";
                    break;
                case "坤兌":
                    stateResult = "00019";
                    break;

                case "巽坤":
                    stateResult = "00020";
                    break;

                case "離震":
                    stateResult = "00021";
                    break;

                case "艮離":
                    stateResult = "00022";
                    break;

                case "艮坤":
                    stateResult = "00023";
                    break;

                case "坤震":
                    stateResult = "00024";
                    break;

                case "乾震":
                    stateResult = "00025";
                    //天雷無妄
                    break;

                case "艮乾":
                    stateResult = "00026";
                    break;

                case "艮震":
                    stateResult = "00027";
                    break;


                case "兌巽":
                    stateResult = "00028";
                    break;


                case "坎坎":
                    stateResult = "00029";
                    break;


                case "離離":
                    stateResult = "00030";
                    break;



                case "兌艮":
                    stateResult = "00031";
                    break;


                case "震巽":
                    stateResult = "00032";
                    break;
                case "乾艮":
                    stateResult = "00033";
                    break;

                case "震乾":
                    stateResult = "00034";
                    break;
                case "離坤":
                    stateResult = "00035";
                    break;
                case "坤離":
                    stateResult = "00036";
                    break;
                case "巽離":
                    stateResult = "00037";
                    break;
                case "離兌":
                    stateResult = "00038";
                    break;
                case "坎艮":
                    stateResult = "00039";
                    break;
                case "震坎":
                    stateResult = "00040";
                    break;

                case "艮兌":
                    stateResult = "00041";
                    break;
                case "巽震":
                    stateResult = "00042";
                    break;
                case "兌乾":
                    stateResult = "00043";
                    break;
                case "乾巽":
                    stateResult = "00044";
                    break;
                case "兌坤":
                    stateResult = "00045";
                    break;
                case "坤巽":
                    stateResult = "00046";
                    break;
                case "兌坎":
                    stateResult = "00047";
                    break;
                case "坎巽":
                    stateResult = "00048";
                    break;
                case "兌離":
                    stateResult = "00049";
                    break;
                case "離巽":
                    stateResult = "00050";
                    break;
                case "震震":
                    stateResult = "00051";
                    break;
                case "艮艮":
                    stateResult = "00052";
                    break;
                case "巽艮":
                    stateResult = "00053";
                    break;
                case "震兌":
                    stateResult = "00054";
                    // 雷澤歸妹
                    break;
                case "震離":
                    stateResult = "00055";
                    break;
                case "離艮":
                    stateResult = "00056";
                    break;
                case "巽巽":
                    stateResult = "00057";
                    break;
                case "兌兌":
                    stateResult = "00058";
                    break;
                case "巽坎":
                    stateResult = "00059";
                    break;
                case "坎兌":
                    stateResult = "00060";
                    break;
                case "巽兌":
                    stateResult = "00061";
                    break;
                case "震艮":
                    stateResult = "00062";
                    break;
                case "坎離":
                    stateResult = "00063";
                    break;
                case "離坎":
                    stateResult = "00064";
                    break;



            }

            return header + stateResult + rear;

        }







    }





    protected void Button1_Click(object sender, EventArgs e)
    {
        string stateResult = "";
        TextBox1.Text = "";
        Divination Div = new Divination(Convert.ToInt32(TextBox2.Text), Convert.ToInt32(TextBox3.Text));
        Div.GetData();

        TextBox1.Text = Div.result.ToString();

        TextBox4.Text = TextBox1.Text.ToString().Substring(0, 3);
        TextBox5.Text = TextBox1.Text.ToString().Substring(3, 3);


        Divination Div_1 = new Divination();

        Label1.Text=Div_1.GetChineseState(TextBox4.Text);

        Label2.Text = Div_1.GetChineseState(TextBox5.Text);



        HyperLink1.NavigateUrl = Div_1.GetStateHyperLink(Label1.Text + Label2.Text);
        HyperLink1.Target = "_Blank";

        HyperLink1.Text=Label1.Text + Label2.Text+" 連結";



    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label3.Text = "";
        Label4.Text = "";

        Divination Div_1 = new Divination();

        Label3.Text = Div_1.GetChineseState(TextBox6.Text);

        Label4.Text = Div_1.GetChineseState(TextBox7.Text);



        HyperLink2.NavigateUrl = Div_1.GetStateHyperLink(Label3.Text + Label4.Text);
        HyperLink2.Target = "_Blank";

        HyperLink2.Text = Label3.Text + Label4.Text + " 連結";


    }
}
