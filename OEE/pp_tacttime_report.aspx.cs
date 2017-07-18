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
using System.Threading;


public partial class OEE_pp_tacttime_report : System.Web.UI.Page
{

    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];

    static Semaphore _Pool = new Semaphore(1, 1);
    static Semaphore _Pool1 = new Semaphore(1, 1);
    //static Semaphore _sem4 = new Semaphore(1, 1);  
   
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    string sql_temp5 = "";
    string sql_temp6 = "";
    string sql_temp7 = "";
    string sql_temp8 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();
    DataSet ds_temp5 = new DataSet();
    DataTable dt_temp = new DataTable();
    DataTable dt_temp1 = new DataTable();
    DataTable dt_temp2 = new DataTable();
    DataTable dt_temp3 = new DataTable();
    DataTable dt_temp4 = new DataTable();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_minus7 = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_sec = DateTime.Now.AddDays(+0).ToString("ss");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    Int32 table_col_num = 0;
    Int32 table_row_num = 0;
    Int32 data_num = 0;


    ArrayList arlist_temp1 = new ArrayList();

    string[] sline ={ "T0ARRAY" };

    string[] sArea ={ "0A-TF", "0A-ETCH", "0A-PHOTO", "0A-TDTEST" };   //0A-ETCH //0A-PHOTO //0A-TDTEST //0A-TF  //AT

    string[] seqid ={ "0APVD", "0APDC", "0AINC", "0AWET", "0ASTR", "0AEXP", "0ACOA", "0ADEV", "0AANO", "0AORO", "0ACVD", "0ADET", "0ATES","0ALSR" };
    //0AEXP 5          0A-TF    0A-TF    0A-TF    0A-ETCH  0A-ETCH  0A-PHOTO  0A-PHOTO  0A-PHOTO 0A-TF   0A-PHOTO   0A-TF   0A-ETCH  0A-TDTEST   0A-TDTEST 
    //0APVD 0           0        1        2       3         4         5        6       7         8        9       10       11          12         13
    //0ASTR 4
    protected void Page_Load(object sender, EventArgs e)
    {
        // only  Continue 0~7
        
      

        func.delete_log_file(Server.MapPath(".") + "\\LOG\\", "*.log", -60);

        #region initial

        //string[] sline ={ "T0ARRAY" };

        //string[] sArea ={ "0A-TF", "0A-ETCH", "0A-PHOTO" };   //0A-ETCH //0A-PHOTO //0A-TDTEST //0A-TF  //AT

        //string[] seqid ={ "0APVD", "0APDC", "0AINC", "0AWET", "0ASTR", "0AEXP", "0ACOA", "0ADEV", "0AANO", "0AORO", "0ACVD","0ADET" };

        //1 layer_row_data(sline[0], sArea[0], seqid[0], 1);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[0], 0.95, sline[0], sArea[0], seqid[0]); // filter 95 %

        //2 layer_row_data(sline[0], sArea[0], seqid[1], 1);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[1], 0.95, sline[0], sArea[0], seqid[1]); // filter 95 %

        //3 layer_row_data(sline[0], sArea[0], seqid[2], 1);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[2], 0.95, sline[0], sArea[0], seqid[2]); // filter 95 %

        //4 layer_row_data(sline[0], sArea[1], seqid[3], 1);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[1] + "_" + seqid[3], 0.95, sline[0], sArea[1], seqid[3]); // filter 95 %

        //5 layer_row_data(sline[0], sArea[1], seqid[4], 1);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[1] + "_" + seqid[4], 0.95, sline[0], sArea[1], seqid[4]); // filter 95 %

        //6 layer_row_data(sline[0], sArea[2], seqid[5], 1);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[2] + "_" + seqid[5], 0.95, sline[0], sArea[2], seqid[5]); // filter 95 %

        //7 layer_row_data(sline[0], sArea[2], seqid[6], 0);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[2] + "_" + seqid[6], 0.95, sline[0], sArea[2], seqid[6]); // filter 95 %

        //8 layer_row_data(sline[0], sArea[2], seqid[7], 0);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[2] + "_" + seqid[7], 0.95, sline[0], sArea[2], seqid[7]); // filter 95 %

        //9 layer_row_data(sline[0], sArea[0], seqid[8], 1);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[8], 0.95, sline[0], sArea[0], seqid[8]); // filter 95 %

        //10 layer_row_data(sline[0], sArea[2], seqid[9], 1);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[2] + "_" + seqid[9], 0.95, sline[0], sArea[2], seqid[9]); // filter 95 %

        //11 layer_row_data(sline[0], sArea[0], seqid[10], 0);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[10], 1, sline[0], sArea[0], seqid[10]); // filter 0 %

        //12 layer_row_data(sline[0], sArea[1], seqid[11], 0);  // First Run function  Create mode source data

        //calcular_mode_data_95(sline[0] + "_" + sArea[1] + "_" + seqid[11], 0.96, sline[0], sArea[1], seqid[11]); // filter 95 %



        //calcular_mode_data_position(3);

        #endregion

        string phase_level = "";
        phase_level = Request.QueryString["phase"];
        //phase_level ="9";

        if (phase_level == "1")
        {
            for (int i = 0; i <= 2 - 1; i++)
            {
                // 0  1
                //Thread t = new Thread(new ThreadStart(DoWork));

                //Thread t = new Thread(DoWork(i)); 

                new Thread(DoWork).Start(i);

                //t.Start(i);
                //_threads.Add(t); 
            }

            Thread.Sleep(200);
          




        
        }

        if (phase_level == "2")
        {
              // 2 3 
            for (int i = 2; i <= 4 - 1; i++)
            {

                new Thread(DoWork).Start(i);

                
            }

            Thread.Sleep(200);

            //_Pool.Release(3); 

        
        }

  
            
         

            

           

       


           if (phase_level == "3")
        {



               //  4 5

            for (int i = 4; i <= 6- 1; i++)
            {


                //Thread t = new Thread(new ThreadStart(DoWork)); 

                //Thread t = new Thread(DoWork(i)); 

                new Thread(DoWork1).Start(i);

                //t.Start(i);
                //_threads.Add(t); 
               
            }


            Thread.Sleep(200);



            //_Pool1.Release(3); 

           

          

            
         

            

           

        }

        if (phase_level == "4")
        {


            //  6   7



            for (int i = 6; i <= 8- 1; i++)
            {

                //Thread t = new Thread(new ThreadStart(DoWork)); 

                //Thread t = new Thread(DoWork(i)); 

                 new Thread(DoWork1).Start(i);

                //t.Start(i); 
                //_threads.Add(t); 
               
            }

            Thread.Sleep(200);

            //_Pool1.Release(3); 













        }

        if (phase_level == "5")
        {
            //    8   9

            for (int i = 8; i <= 10 - 1; i++)
            {

                //Thread t = new Thread(new ThreadStart(DoWork)); 

                //Thread t = new Thread(DoWork(i)); 

                new Thread(DoWork1).Start(i);

                //t.Start(i); 
                //_threads.Add(t); 

            }

            Thread.Sleep(200);

            //_Pool1.Release(3); 

        }


        if (phase_level == "6")
        {
            //    10   11

            for (int i = 10; i <= 12 - 1; i++)
            {

                //Thread t = new Thread(new ThreadStart(DoWork)); 

                //Thread t = new Thread(DoWork(i)); 

                new Thread(DoWork1).Start(i);

                //t.Start(i); 
                //_threads.Add(t); 

            }

            Thread.Sleep(200);

            //_Pool1.Release(3); 

        }
        if (phase_level == "7")
        {
            //    12   13

            for (int i = 12; i <= 14 - 1; i++)
            {

                //Thread t = new Thread(new ThreadStart(DoWork)); 

                //Thread t = new Thread(DoWork(i)); 

                new Thread(DoWork1).Start(i);

                //t.Start(i); 
                //_threads.Add(t); 

            }

            Thread.Sleep(200);

            //_Pool1.Release(3); 

        }



        if (phase_level == "9")
        {
            //    10   11

            oscar_thread10();
            //oscar_thread11();
        }

        oscar_thread_sleep1();



        //show_data("TF", seqid[0]);//
        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");

        //GridView1.HeaderRow.Visible = false;



        }

    public void DoWork(object id)
    {

        // Wait on a semaphore slot to become available 

        string tmp = "";
        tmp = id.ToString() + Thread.CurrentThread.ManagedThreadId.ToString() + "_" + Thread.CurrentThread.ThreadState.ToString();
        Response.Write(tmp+"<BR>");
        _Pool.WaitOne();

        //int padding = Interlocked.Add(ref _padding, 100); 
       
        //Response.Write("Acquired slot... <br>");

        #region MyRegion
        if (Convert.ToInt32(id) == 0)
        {
            ThreadStart simplest1 = new ThreadStart(oscar_thread1);
            Thread thread1 = new Thread(simplest1);
            //Response.Write("0<br>");
            thread1.Start();
            thread1.Join();

        }
        if (Convert.ToInt32(id) == 1)
        {
            ThreadStart simplest2 = new ThreadStart(oscar_thread2);
            Thread thread2 = new Thread(simplest2);
            //Response.Write("1<br>");
            thread2.Start();
            thread2.Join();

        }
        if (Convert.ToInt32(id) == 2)
        {

            ThreadStart simplest3 = new ThreadStart(oscar_thread3);
            Thread thread3 = new Thread(simplest3);
           // Response.Write("2<br>");
            thread3.Start();
            thread3.Join();

        }
        if (Convert.ToInt32(id) == 3)
        {
            ThreadStart simplest4 = new ThreadStart(oscar_thread4);
            Thread thread4 = new Thread(simplest4);

            thread4.Start();
           // Response.Write("3<br>");
            //oscar_thread_sleep();
            thread4.Join();
        }
        if (Convert.ToInt32(id) == 4)
        {
            ThreadStart simplest5 = new ThreadStart(oscar_thread5);
            Thread thread5 = new Thread(simplest5);

            thread5.Start();
           // Response.Write("4<br>");
            //oscar_thread_sleep();
            thread5.Join();
        }
        if (Convert.ToInt32(id) == 5)
        {

            ThreadStart simplest6 = new ThreadStart(oscar_thread6);
            Thread thread6 = new Thread(simplest6);
            thread6.Start();
            //Response.Write("5<br>");
            //thread12.Start();
            thread6.Join();
        }

       
        if (Convert.ToInt32(id) == 6)
        {

            ThreadStart simplest7 = new ThreadStart(oscar_thread7);
            Thread thread7 = new Thread(simplest7);
            thread7.Start();
            //Response.Write("6<br>");
            //thread12.Start();
            thread7.Join();

        }
        if (Convert.ToInt32(id) == 7)
        {

            ThreadStart simplest8 = new ThreadStart(oscar_thread8);
            Thread thread8 = new Thread(simplest8);
            thread8.Start();
            //Response.Write("7<br>");
            //thread12.Start();
            thread8.Join();

        }

        if (Convert.ToInt32(id) == 8)
        {

            ThreadStart simplest9 = new ThreadStart(oscar_thread9);
            Thread thread9 = new Thread(simplest9);
            thread9.Start();
            //Response.Write("9<br>");
            //thread12.Start();
            thread9.Join();
        }

        if (Convert.ToInt32(id) == 9)
        {

            ThreadStart simplest10 = new ThreadStart(oscar_thread10);
            Thread thread10= new Thread(simplest10);
            thread10.Start();
            //Response.Write("10<br>");
            //thread12.Start();
 thread10.Join();

        }

        if (Convert.ToInt32(id) == 10)
        {

            ThreadStart simplest11 = new ThreadStart(oscar_thread11);
            Thread thread11 = new Thread(simplest11);
            thread11.Start();
            //Response.Write("11<br>");
            //thread12.Start();
 thread11.Join();

        }

        if (Convert.ToInt32(id) == 11)
        {

            ThreadStart simplest12 = new ThreadStart(oscar_thread12);
            Thread thread12 = new Thread(simplest12);
            thread12.Start();
            //Response.Write("12<br>");
            thread12.Join();
            //thread12.Start();

        }

        #endregion

        //Response.Write("Released slot... <br>");

        Thread.Sleep(3000 ); 

        //Response.Write("last " + tmp+"<br>");
        // Release the semaphore slot 

        _Pool.Release();
       
    }

    public void DoWork1(object id)
    {

        // Wait on a semaphore slot to become available 

        string tmp = "";
        tmp = id.ToString() + Thread.CurrentThread.ManagedThreadId.ToString() + "_" + Thread.CurrentThread.ThreadState.ToString();
        Response.Write(tmp+"<BR>");
        _Pool1.WaitOne();
      
        //int padding = Interlocked.Add(ref _padding, 100); 

         Response.Write("Acquired slot... <br>");

        #region MyRegion
        if (Convert.ToInt32(id) == 0)
        {
            ThreadStart simplest1 = new ThreadStart(oscar_thread1);
            Thread thread1 = new Thread(simplest1);
            Response.Write("0<br>");
            thread1.Start();
            thread1.Join();

        }
        if (Convert.ToInt32(id) == 1)
        {
            ThreadStart simplest2 = new ThreadStart(oscar_thread2);
            Thread thread2 = new Thread(simplest2);
            Response.Write("1<br>");
            thread2.Start();
            thread2.Join();

        }
        if (Convert.ToInt32(id) == 2)
        {

            ThreadStart simplest3 = new ThreadStart(oscar_thread3);
            Thread thread3 = new Thread(simplest3);
            Response.Write("2<br>");
            thread3.Start();
            thread3.Join();

        }
        if (Convert.ToInt32(id) == 3)
        {
            ThreadStart simplest4 = new ThreadStart(oscar_thread4);
            Thread thread4 = new Thread(simplest4);

            thread4.Start();
            Response.Write("3<br>");
            //oscar_thread_sleep();
            thread4.Join();
        }
        if (Convert.ToInt32(id) == 4)
        {
            ThreadStart simplest5 = new ThreadStart(oscar_thread5);
            Thread thread5 = new Thread(simplest5);

            thread5.Start();
            Response.Write("4<br>");
            //oscar_thread_sleep();
            thread5.Join();
        }
        if (Convert.ToInt32(id) == 5)
        {

            ThreadStart simplest6 = new ThreadStart(oscar_thread6);
            Thread thread6 = new Thread(simplest6);
            thread6.Start();
            Response.Write("5<br>");
            //thread12.Start();
            thread6.Join();
        }

       
        if (Convert.ToInt32(id) == 6)
        {

            ThreadStart simplest7 = new ThreadStart(oscar_thread7);
            Thread thread7 = new Thread(simplest7);
            thread7.Start();
            Response.Write("6<br>");
            //thread12.Start();
            thread7.Join();

        }
        if (Convert.ToInt32(id) == 7)
        {

            ThreadStart simplest8 = new ThreadStart(oscar_thread8);
            Thread thread8 = new Thread(simplest8);
            thread8.Start();
            Response.Write("7<br>");
            //thread12.Start();
            thread8.Join();

        }

        if (Convert.ToInt32(id) == 8)
        {
            //"0AANO"
            ThreadStart simplest9 = new ThreadStart(oscar_thread9);
            Thread thread9 = new Thread(simplest9);
            thread9.Start();
            Response.Write("9<br>");
            //thread12.Start();
            thread9.Join();
        }

        if (Convert.ToInt32(id) == 9)
        {

            ThreadStart simplest10 = new ThreadStart(oscar_thread10);
            Thread thread10= new Thread(simplest10);
            thread10.Start();
            Response.Write("10<br>");
            //thread12.Start();
 thread10.Join();

        }

        if (Convert.ToInt32(id) == 10)
        {

            ThreadStart simplest11 = new ThreadStart(oscar_thread11);
            Thread thread11 = new Thread(simplest11);
            thread11.Start();
            Response.Write("11<br>");
            //thread12.Start();
 thread11.Join();

        }

        if (Convert.ToInt32(id) == 11)
        {

            ThreadStart simplest12 = new ThreadStart(oscar_thread12);
            Thread thread12 = new Thread(simplest12);
            thread12.Start();
            Response.Write("12<br>");
            thread12.Join();
            //thread12.Start();

        }
        if (Convert.ToInt32(id) == 12)
        {

            ThreadStart simplest13 = new ThreadStart(oscar_thread13);
            Thread thread13 = new Thread(simplest13);
            thread13.Start();
            Response.Write("12<br>");
            thread13.Join();
            //thread12.Start();

        }


        if (Convert.ToInt32(id) == 13)
        {

            ThreadStart simplest14 = new ThreadStart(oscar_thread14);
            Thread thread14 = new Thread(simplest14);
            thread14.Start();
            Response.Write("12<br>");
            thread14.Join();
            //thread12.Start();

        }
         #endregion

        //Response.Write("Released slot... <br>");

        Thread.Sleep(3000); 


        //Response.Write("last " + tmp+"<BR>");
        // Release the semaphore slot 

        _Pool1.Release();
       
    }


    public Int32 Getpilotremovenum(string eqp)
    {
        // Added 0AANO/0AORO    get remove glass  2  20131031 by oscar
        // get remove glass num 20130321 by oscar  
        sql_temp8 = @"
select  case when ot1.remove_pilot='Yes' and ot1.eq='0AANO' then 2 
             when ot1.remove_pilot='Yes' and ot1.eq='0AORO' then 2 
             when ot1.remove_pilot='Yes' then 1
else 0
end remove_count
 from (

select t.eq,t.remove_pilot,count(t.moduale) from pp_tactime_config t
group by t.eq,t.remove_pilot


) ot1
where substr(ot1.eq,0,5)='{0}'

";

        sql_temp8 = string.Format(sql_temp8, eqp);


        ds_temp5 = func.get_dataSet_access(sql_temp8, conn1);

        if (ds_temp5.Tables[0].Rows.Count > 0 )
        {
            if (ds_temp5.Tables[0].Rows[0][0].ToString().Equals("2"))
            {
                return 2;
            }
            else if (ds_temp5.Tables[0].Rows[0][0].ToString().Equals("1"))
            {
                return 1;
            }
            else return 0;
            
            

        }
        else
        {
            return 0;
        
        }
    } 
   




    public void Simplest_oscar()
    {
        string oscar_sleep = "1234";






    }
    private void oscar_thread_sleep()
    {

        ThreadStart simplestqq = new ThreadStart(Simplest_oscar);
        Thread thread_abc = new Thread(simplestqq);
        thread_abc.Start();

        Thread.Sleep(300000);

    }

    private void oscar_thread_sleep1()
    {

        ThreadStart simplestqq = new ThreadStart(Simplest_oscar);
        Thread thread_abc = new Thread(simplestqq);
        thread_abc.Start();
        //Thread.Sleep(1500000);
        Thread.Sleep(1800000);

    }

   

    private void oscar_thread1()
    {
        layer_row_data(sline[0], sArea[0], seqid[0], Getpilotremovenum(seqid[0]));  // First Run function  Create mode source data T0ARRAY/0A-TF/0APVD

        calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[0], 0.95, sline[0], sArea[0], seqid[0]); // filter 95 %


    }
    private void oscar_thread2()
    {
        layer_row_data(sline[0], sArea[0], seqid[1], Getpilotremovenum(seqid[1]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[1], 0.95, sline[0], sArea[0], seqid[1]); // filter 95 %

    }
    //0AINC100
    private void oscar_thread3()
    {
        layer_row_data(sline[0], sArea[0], seqid[2], Getpilotremovenum(seqid[2]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[2], 0.95, sline[0], sArea[0], seqid[2]); // filter 95 %

    }

    private void oscar_thread4()
    {
        layer_row_data(sline[0], sArea[1], seqid[3], Getpilotremovenum(seqid[3]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[1] + "_" + seqid[3], 0.95, sline[0], sArea[1], seqid[3]); // filter 95 %

    }
    private void oscar_thread5()
    {
        layer_row_data(sline[0], sArea[1], seqid[4], Getpilotremovenum(seqid[4]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[1] + "_" + seqid[4], 0.95, sline[0], sArea[1], seqid[4]); // filter 95 %
    }
    private void oscar_thread6()
    {
        layer_row_data(sline[0], sArea[2], seqid[5], Getpilotremovenum(seqid[5]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[2] + "_" + seqid[5], 0.95, sline[0], sArea[2], seqid[5]); // filter 95 %
    }

    private void oscar_thread7()
    {
        layer_row_data(sline[0], sArea[2], seqid[6], Getpilotremovenum(seqid[6]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[2] + "_" + seqid[6], 0.95, sline[0], sArea[2], seqid[6]); // filter 95 %
    }
    private void oscar_thread8()     //oscar
    {
        layer_row_data(sline[0], sArea[2], seqid[7], Getpilotremovenum(seqid[7]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[2] + "_" + seqid[7], 0.95, sline[0], sArea[2], seqid[7]); // filter 95 %

    }

    private void oscar_thread9()     // oscar1230  0AANO  20131231 no Data  Issue
    {
        layer_row_data(sline[0], sArea[0], seqid[8], Getpilotremovenum(seqid[8]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[8], 0.95, sline[0], sArea[0], seqid[8]); // filter 95 %
    }

    private void oscar_thread10()  //0AORO
    {
        layer_row_data(sline[0], sArea[2], seqid[9], Getpilotremovenum(seqid[9]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[2] + "_" + seqid[9], 0.95, sline[0], sArea[2], seqid[9]); // filter 95 %
    }

    private void oscar_thread11() //0ACVD
    {
        layer_row_data(sline[0], sArea[0], seqid[10], Getpilotremovenum(seqid[10]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[0] + "_" + seqid[10], 1, sline[0], sArea[0], seqid[10]); // filter 0 %
    }

    private void oscar_thread12()
    {
        layer_row_data(sline[0], sArea[1], seqid[11], Getpilotremovenum(seqid[11]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[1] + "_" + seqid[11], 0.95, sline[0], sArea[1], seqid[11]); // filter 95 %  T0ARRAY/0A-TF/0APVD
    }

    private void oscar_thread13()
    {
        layer_row_data(sline[0], sArea[3], seqid[12], Getpilotremovenum(seqid[12]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[3] + "_" + seqid[12], 0.95, sline[0], sArea[3], seqid[12]); // filter 95 %  T0ARRAY/0A-TDTEST/0ATES
    }

    private void oscar_thread14()
    {
        layer_row_data(sline[0], sArea[3], seqid[13], Getpilotremovenum(seqid[13]));  // First Run function  Create mode source data

        calcular_mode_data_95(sline[0] + "_" + sArea[3] + "_" + seqid[13], 0.95, sline[0], sArea[3], seqid[13]); // filter 95 %  T0ARRAY/0A-TDTEST/0ATES
    }

    //string[] sline ={ "T0ARRAY" };

    //string[] sArea ={ "0A-TF", "0A-ETCH", "0A-PHOTO" };   //0A-ETCH //0A-PHOTO //0A-TDTEST //0A-TF  //AT

    //string[] seqid ={ "0APVD", "0APDC", "0AINC", "0AWET", "0ASTR", "0AEXP", "0ACOA", "0ADEV", "0AANO", "0AORO", "0ACVD", "0ADET" };


    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        Int32 rownum = table_row_num;
        Int32 colnum = table_col_num;

        Int32 counter = 1;

        //merge column  RowSpan
        for (int i = 0; i <= colnum - 1; i++)
        {

            counter = 1;
            for (int j = 1; j <= gv.Rows.Count - 1; j++)
            {

                if (GridView1.Rows[j].Cells[i].Text.Trim() == GridView1.Rows[(j - 1)].Cells[i].Text.Trim())
                {
                    counter++;
                    GridView1.Rows[j - counter + 1].Cells[i].RowSpan = counter;


                    GridView1.Rows[j].Cells[i].Visible = false;


                }

                else
                {
                    counter = 1;
                    GridView1.Rows[j].Cells[i].RowSpan = counter;

                }



            }








        }



        // merge row  ColumnSpan
        for (int i = 0; i <= gv.Rows.Count - 1; i++)
        {

            counter = 1;
            for (int j = 1; j <= colnum - 1; j++)
            {

                if (GridView1.Rows[i].Cells[j].Text.Trim() == GridView1.Rows[i].Cells[j - 1].Text.Trim())
                {
                    counter++;
                    GridView1.Rows[i].Cells[j - counter + 1].ColumnSpan = counter;


                    GridView1.Rows[i].Cells[j].Visible = false;


                }

                else
                {
                    counter = 1;
                    GridView1.Rows[i].Cells[j].ColumnSpan = counter;

                }



            }








        }


    }

    private void layer_row_data(string line, string area, string eq, Int32 remove_pivot_glass_num)
    {
        // 20130118 Modify by Oscar   filter main equipment sequence 0 
        sql_temp = @"
select ot3.line,ot3.moduale,substr(ot3.equipmentid,0,8) as equipmentid,ot3.product_type,ot3.layer,
   sum(  case when ot3.remove_pilot='Yes' then 1 else 0 end ) as pilot_flag

 from (

select ot1.*,ot2.moduale,ot2.product_type,ot2.layer,ot2.up_bound,ot2.low_bound,ot2.remove_pilot from (


select ot2.line,ot2.equipmentid,ot2.enddatetime,ot2.productid,ot2.lotid,ot2.glassid,ot2.stepid,ot2.endtacttime from empaglasshistory ot2
where ot2.equipmentid in 
(

select ot1.equipmentid from (
select t.line,t.equipmentid,t.area,substr(t.equipmentid,3,3) eq from equipment t
where t.line='{0}' and  t.area='{2}' and t.modulelevel='0' 
)ot1
where ot1.eq='{1}'
)
and ot2.ENDDATETIME>(select to_char(next_day(sysdate,1)-14,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.ENDDATETIME<=(select to_char(next_day(sysdate,1)-7,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.line='{0}'

order by ot2.line,ot2.equipmentid,ot2.ENDDATETIME

)ot1,
(
select tt.moduale,tt.eq,tt.product,tt.product_type,tt.step,tt.layer,tt.up_bound,tt.low_bound,tt.remove_pilot from pp_tactime_config tt

) ot2

where substr(ot1.equipmentid,0,5)=ot2.eq(+)
and  ot1.productid=ot2.product(+)
and ot1.stepid=ot2.step(+)


)ot3

{3}
group by ot3.line,ot3.moduale,substr(ot3.equipmentid,0,8),ot3.product_type,ot3.layer





";
        if (!eq.Equals("0AINC"))
        {
            sql_temp = string.Format(sql_temp, line, eq.Substring(2, 3), area, "where ot3.moduale is not null ");

        }
        else
        {

            sql_temp = string.Format(sql_temp, line, eq.Substring(2, 3), area, " ");
        }


        ds_temp = func.get_dataSet_access(sql_temp, conn1);
       
        data_num= ds_temp.Tables[0].Rows.Count;
        
      

        GridView1.DataSource = ds_temp.Tables[0];
        GridView1.DataBind();
        // T0ARRAY  0A-TF  0APVD
        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++) // layer distinct data TF/PVD
        {
            if ((ds_temp.Tables[0].Rows[i]["moduale"].ToString().Equals("")) && (ds_temp.Tables[0].Rows[i]["product_type"].ToString().Equals("")) && (ds_temp.Tables[0].Rows[i]["layer"].ToString().Equals("")))
            {
                // OAINC 
                sql_temp1 = @"

           select ot4.* from 

(

select ot1.*,ot2.moduale,ot2.product_type,ot2.layer,ot2.up_bound,ot2.low_bound,ot2.remove_pilot from (


select ot2.line,ot2.equipmentid,ot2.enddatetime,ot2.productid,ot2.lotid,ot2.glassid,ot2.stepid,ot2.endtacttime from empaglasshistory ot2
where ot2.equipmentid in 
(

select ot1.equipmentid from (
select t.line,t.equipmentid,t.area,substr(t.equipmentid,3,3) eq from equipment t
where t.line='{4}' and  t.area='{5}' and t.modulelevel='0' 
)ot1
where ot1.eq=substr('{6}',3,6)
)
and ot2.ENDDATETIME>=(select to_char(next_day(sysdate,1)-14,'yyyy-MM-dd')||' 07:00:00' from dual)
and ot2.ENDDATETIME<=(select to_char(next_day(sysdate,1)-7,'yyyy-MM-dd')||' 07:00:00' from dual)
and ot2.line='{4}'

order by ot2.line,ot2.equipmentid,ot2.ENDDATETIME

)ot1,
(
select tt.moduale,tt.eq,tt.product,tt.product_type,tt.step,tt.layer,tt.up_bound,tt.low_bound,tt.remove_pilot from pp_tactime_config tt

) ot2

where substr(ot1.equipmentid,0,5)=ot2.eq(+)
and  ot1.productid=ot2.product(+)
and ot1.stepid=ot2.step(+)


) ot4
where  ot4.moduale is null 
and ot4.equipmentid like '{1}%' 
and ot4.product_type is null
and ot4.layer is null
order by ot4.line,ot4.equipmentid,ot4.ENDDATETIME

";

            }


            else
            {

                sql_temp1 = @"

           select ot4.* from 

(

select ot1.*,ot2.moduale,ot2.product_type,ot2.layer,ot2.up_bound,ot2.low_bound,ot2.remove_pilot from (


select ot2.line,ot2.equipmentid,ot2.enddatetime,ot2.productid,ot2.lotid,ot2.glassid,ot2.stepid,ot2.endtacttime from empaglasshistory ot2
where ot2.equipmentid in 
(

select ot1.equipmentid from (
select t.line,t.equipmentid,t.area,substr(t.equipmentid,3,3) eq from equipment t
where t.line='{4}' and  t.area='{5}' and t.modulelevel='0' 
)ot1
where ot1.eq=substr('{6}',3,6)
)
and ot2.ENDDATETIME>(select to_char(next_day(sysdate,1)-14,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.ENDDATETIME<=(select to_char(next_day(sysdate,1)-7,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.line='{4}'
and ot2.sequence=0
order by ot2.line,ot2.equipmentid,ot2.ENDDATETIME

)ot1,
(
select tt.moduale,tt.eq,tt.product,tt.product_type,tt.step,tt.layer,tt.up_bound,tt.low_bound,tt.remove_pilot from pp_tactime_config tt

) ot2

where substr(ot1.equipmentid,0,5)=ot2.eq(+)
and  ot1.productid=ot2.product(+)
and ot1.stepid=ot2.step(+)


) ot4
where  ot4.moduale='{0}' 
and ot4.equipmentid like '{1}%' 
and ot4.product_type='{2}' 
and ot4.layer='{3}'
order by ot4.line,ot4.equipmentid,ot4.ENDDATETIME

";

            }



            // Check order by Endtacttime 20130121 by  oscar

            sql_temp1 = string.Format(sql_temp1, ds_temp.Tables[0].Rows[i]["moduale"].ToString(), ds_temp.Tables[0].Rows[i]["equipmentid"].ToString(), ds_temp.Tables[0].Rows[i]["product_type"].ToString(), ds_temp.Tables[0].Rows[i]["layer"].ToString(), line, area, eq);

            ds_temp2 = func.get_dataSet_access(sql_temp1, conn1);

            // 0AINC  fillup MODUAL=TF  PRODUCT_TYPE=INC LAYER='INC'  up_bound='-'  low_bound='-'  remove_pilot='1' 
            if ((ds_temp.Tables[0].Rows[i]["moduale"].ToString().Equals("")) && (ds_temp.Tables[0].Rows[i]["product_type"].ToString().Equals("")) && (ds_temp.Tables[0].Rows[i]["layer"].ToString().Equals("")))
            {

                for (int j = 0; j <= ds_temp2.Tables[0].Rows.Count - 1; j++)
                {

                    ds_temp2.Tables[0].Rows[j]["moduale"] = "TF";
                    ds_temp2.Tables[0].Rows[j]["product_type"] = "INC";
                    ds_temp2.Tables[0].Rows[j]["layer"] = "INC";
                    ds_temp2.Tables[0].Rows[j]["up_bound"] = "-";
                    ds_temp2.Tables[0].Rows[j]["low_bound"] = "-";
                    ds_temp2.Tables[0].Rows[j]["remove_pilot"] = "Yes";


                }

            }


            if (data_num >0)
            {

                if (ds_temp2.Tables[0].Rows[0]["equipmentid"].ToString().Equals("0AORO100"))
                {
                    remove_pivot_glass_num = 1;
                }
                remove_pivot(ds_temp2.Tables[0], remove_pivot_glass_num, line, area, eq);  // filter  first glass  

                calcular_mode_data(line, area, eq);

            }
           

            






       

          


        }

    }
    private DataTable nonremove_pivot(DataTable dt) // Remove pivot glass data
    {

        #region remove_pivot
        // NonRemove first glass tacttime
        #endregion
        string temp_lotid = "";



        for (int j = 0; j <= dt.Rows.Count - 1; j++) //insert temp physical table
        {
            sql_temp3 = @"

   insert into pp_frequncy_table
  (line, equipmentid, enddatetime, productid, lotid, glassid, stepid, endtacttime, moduale, product_type, layer, up_bound, low_bound, remove_pilot,dttm)
values
  ('{0}', '{1}', to_date('{2}','yyyy-MM-dd HH24:mi:ss'), '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', {11}, {12}, '{13}',sysdate)

";

            sql_temp3 = string.Format(sql_temp3, dt.Rows[j]["line"].ToString(), dt.Rows[j]["equipmentid"].ToString(), dt.Rows[j]["enddatetime"].ToString(), dt.Rows[j]["productid"].ToString(), dt.Rows[j]["lotid"].ToString(), dt.Rows[j]["glassid"].ToString(), dt.Rows[j]["stepid"].ToString(), dt.Rows[j]["endtacttime"].ToString(), dt.Rows[j]["moduale"].ToString(), dt.Rows[j]["product_type"].ToString(), dt.Rows[j]["layer"].ToString(), dt.Rows[j]["up_bound"].ToString().Replace("-", "0"), dt.Rows[j]["low_bound"].ToString().Replace("-", "0"), dt.Rows[j]["remove_pilot"].ToString());
            func.get_sql_execute(sql_temp3, conn1);
        }

        return dt;


    }

    private DataTable remove_pivot(DataTable dt, Int32 glass_number, string line, string area, string eq) // Remove pivot glass data
    {

        #region remove_pivot
        // Remove first glass tacttime
        #endregion
        string temp_lotid = "";


        //#region build DataTable structure
        //DataTable dt_filter = new DataTable();
        //DataColumn dc;
        //for (int i = 0; i <= dt.Columns.Count - 1; i++)
        //{
        //    dc = new DataColumn();
           
        //    dc.ColumnName = dt.Columns[i].ColumnName.ToString();
        //    dt_filter.Columns.Add(dc);
        //}


        //#endregion

       




        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {

            DataRow dr = dt.NewRow();

            
            //if (i == 0)
            //{
            //    temp_lotid = dt.Rows[i]["lotid"].ToString();
            //}
            //else
            //{
            //    // not first glass add to filter table
            //    if (dt.Rows[i]["lotid"].ToString().Equals(temp_lotid))
            //   {
            //        for (int k = 0; k <= dt.Columns.Count-1; k++)
            //    {
            //        dr[k] = dt.Rows[i][k].ToString();
            //    }


            //    dt_filter.Rows.Add(dr);
               
            //   }
                
              

            
            //}

           
             //first data think it first glass  Remove it
            

                if (!dt.Rows[i]["lotid"].ToString().Equals(temp_lotid))
                {
                    temp_lotid = dt.Rows[i]["lotid"].ToString();


                       for (int k = 0; k <= glass_number - 1; k++)
                    {
                       
                            dt.Rows.Remove(dt.Rows[i]);
                       
                  
                　　}

                    i--;

                } 





        }

        // fileter data Reassign to dt ,check remove wrong glassid

        

        #region Create virtual table

        //check table alive
        sql_temp8 = @"
        select count(t.owner) as counter from all_tables t
 where t.table_name=upper('{0}')
      
";
        sql_temp8 = string.Format(sql_temp8, "PFT_" + line + area.Replace("-", "_") + eq);

        dt_temp4 = func.get_dataSet_access(sql_temp8, conn1).Tables[0];

        if (Convert.ToInt16(dt_temp4.Rows[0][0].ToString()) <= 0)
        {


            sql_temp7 = @" create table {0}
(
  LINE         VARCHAR2(500),
  EQUIPMENTID  VARCHAR2(500),
  ENDDATETIME  DATE,
  PRODUCTID    VARCHAR2(500),
  LOTID        VARCHAR2(500),
  GLASSID      VARCHAR2(500),
  STEPID       VARCHAR2(500),
  ENDTACTTIME  NUMBER,
  MODUALE      VARCHAR2(500),
  PRODUCT_TYPE VARCHAR2(500),
  LAYER        VARCHAR2(500),
  UP_BOUND     NUMBER,
  LOW_BOUND    NUMBER,
  REMOVE_PILOT VARCHAR2(500),
  DTTM         DATE
)

";
            //PP_Frequnce_Data  MONITOR ROWDATA
            sql_temp7 = string.Format(sql_temp7, "PFT_" + line + area.Replace("-", "_") + eq);
           

            func.get_sql_execute(sql_temp7, conn1);




        }




        #endregion




        for (int j = 0; j <= dt.Rows.Count - 1; j++) //insert temp physical table
        {
            sql_temp3 = @"

   insert into {14}
  (line, equipmentid, enddatetime, productid, lotid, glassid, stepid, endtacttime, moduale, product_type, layer, up_bound, low_bound, remove_pilot,dttm)
values
  ('{0}', '{1}', to_date('{2}','yyyy-MM-dd HH24:mi:ss'), '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', {11}, {12}, '{13}',sysdate)

";

            sql_temp3 = string.Format(sql_temp3, dt.Rows[j]["line"].ToString(), dt.Rows[j]["equipmentid"].ToString(), dt.Rows[j]["enddatetime"].ToString(), dt.Rows[j]["productid"].ToString(), dt.Rows[j]["lotid"].ToString(), dt.Rows[j]["glassid"].ToString(), dt.Rows[j]["stepid"].ToString(), dt.Rows[j]["endtacttime"].ToString(), dt.Rows[j]["moduale"].ToString(), dt.Rows[j]["product_type"].ToString(), dt.Rows[j]["layer"].ToString(), dt.Rows[j]["up_bound"].ToString().Replace("-", "10000000"), dt.Rows[j]["low_bound"].ToString().Replace("-", "0"), dt.Rows[j]["remove_pilot"].ToString(), "PFT_" + line + area.Replace("-", "_") + eq );
            func.get_sql_execute(sql_temp3, conn1);


 //CHECK PP_FREQUENCE_DATA

            sql_temp5 = @"

   insert into {14}
  (line, equipmentid, enddatetime, productid, lotid, glassid, stepid, endtacttime, moduale, product_type, layer, up_bound, low_bound, remove_pilot,dttm)
values
  ('{0}', '{1}', to_date('{2}','yyyy-MM-dd HH24:mi:ss'), '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', {11}, {12}, '{13}',sysdate)

";
            sql_temp5 = string.Format(sql_temp5, dt.Rows[j]["line"].ToString(), dt.Rows[j]["equipmentid"].ToString(), dt.Rows[j]["enddatetime"].ToString(), dt.Rows[j]["productid"].ToString(), dt.Rows[j]["lotid"].ToString(), dt.Rows[j]["glassid"].ToString(), dt.Rows[j]["stepid"].ToString(), dt.Rows[j]["endtacttime"].ToString(), dt.Rows[j]["moduale"].ToString(), dt.Rows[j]["product_type"].ToString(), dt.Rows[j]["layer"].ToString(), dt.Rows[j]["up_bound"].ToString().Replace("-", "10000000"), dt.Rows[j]["low_bound"].ToString().Replace("-", "0"), dt.Rows[j]["remove_pilot"].ToString(), "pp_frequncy_table");
            func.get_sql_execute(sql_temp5, conn1);

        }

        return dt;


    }


    private void calcular_mode_data(string line, string area, string eq)  //source table pp_frequncy_table  filter outlier data  by low_bound, up_bound   and filter endtactime=0
    {
    
        func.write_log("tacttime_calcular_mode_data Start", Server.MapPath(".") + "\\LOG\\", "log");

        string sql_temp2 = @"

       select t1.*,t1.endtacttime1*t1.counter as counter2  from (

select t.moduale,substr(t.equipmentid,0,8)as equiptype,t.productid,t.product_type,t.layer,t.endtacttime1,count(t.line) as counter from (

select line, equipmentid, enddatetime, productid, lotid, glassid, stepid, endtacttime,
case when (t.endtacttime<t.low_bound or t.endtacttime>t.up_bound) then 0
     else t.endtacttime end endtacttime1
, moduale, product_type, layer, up_bound, low_bound, remove_pilot, dttm from {0} t

) t
where t.endtacttime1>0
group by t.moduale,substr(t.equipmentid,0,8),t.product_type,t.layer,t.endtacttime1,t.productid
order by t.moduale,substr(t.equipmentid,0,8),t.product_type,t.layer,count(t.line) desc,t.endtacttime1
) t1


";
        
        sql_temp2 = string.Format(sql_temp2, "PFT_" + line + area.Replace("-", "_") + eq);
        ds_temp2 = func.get_dataSet_access(sql_temp2, conn1);



        #region create Virtual table

        sql_temp8 = @"
        select count(t.owner) as counter from all_tables t
 where t.table_name=upper('{0}')
      
";
        sql_temp8 = string.Format(sql_temp8, "PMD" + line + area.Replace("-", "_") + eq);

        dt_temp4 = func.get_dataSet_access(sql_temp8, conn1).Tables[0];

         //Create PMD Table

        if (Convert.ToInt16(dt_temp4.Rows[0][0].ToString()) <= 0)
        {
            sql_temp7 = @" create table {0}
(
  MODUALE      VARCHAR2(500),
  EQUIPTYPE    VARCHAR2(500),
  PRODUCT_TYPE VARCHAR2(500),
  LAYER        VARCHAR2(500),
  ENDTACTTIME1 VARCHAR2(500),
  COUNTER      NUMBER,
  COUNTER1     NUMBER,
  DTTM         DATE,
  PRODUCTID    VARCHAR2(500)
)

";
            //PP_mode_Data
            sql_temp7 = string.Format(sql_temp7, "PMD" + line + area.Replace("-", "_") + eq);

            func.get_sql_execute(sql_temp7, conn1);


         

        }




        #endregion




        // Add PMD data TO PMD Table

        for (int i = 0; i <= ds_temp2.Tables[0].Rows.Count - 1; i++)
        {

            sql_temp3 = @"

insert into {7}
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm,productid)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate,'{8}')


";
            if (ds_temp2.Tables[0].Rows[i]["productid"].ToString().Equals(""))
            {
                ds_temp2.Tables[0].Rows[i]["productid"] = "NA";
            
            }

            sql_temp3 = string.Format(sql_temp3, ds_temp2.Tables[0].Rows[i]["moduale"].ToString(), ds_temp2.Tables[0].Rows[i]["equiptype"].ToString(), ds_temp2.Tables[0].Rows[i]["product_type"].ToString(), ds_temp2.Tables[0].Rows[i]["layer"].ToString(), ds_temp2.Tables[0].Rows[i]["endtacttime1"].ToString(), ds_temp2.Tables[0].Rows[i]["counter"].ToString(), ds_temp2.Tables[0].Rows[i]["counter2"].ToString(), "PMD" + line + area.Replace("-", "_") + eq, ds_temp2.Tables[0].Rows[i]["productid"].ToString());
            //PMD LINE AREA EQ

            func.get_sql_execute(sql_temp3, conn1);

            // pp_mode_data_his have null data in product_id
            sql_temp6 = @"

insert into pp_mode_data_his
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm,productid)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate,'{7}')


";

            sql_temp6 = string.Format(sql_temp6, ds_temp2.Tables[0].Rows[i]["moduale"].ToString(), ds_temp2.Tables[0].Rows[i]["equiptype"].ToString(), ds_temp2.Tables[0].Rows[i]["product_type"].ToString(), ds_temp2.Tables[0].Rows[i]["layer"].ToString(), ds_temp2.Tables[0].Rows[i]["endtacttime1"].ToString(), ds_temp2.Tables[0].Rows[i]["counter"].ToString(), ds_temp2.Tables[0].Rows[i]["counter2"].ToString(), ds_temp2.Tables[0].Rows[i]["productid"].ToString());


            func.get_sql_execute(sql_temp6, conn1);

        }

        //check table alive drop PFT table


        sql_temp8 = @"
        select count(t.owner) as counter from all_tables t
 where t.table_name=upper('{0}')
      
";
        sql_temp8 = string.Format(sql_temp8, "PFT_" + line + area.Replace("-", "_") + eq);

        dt_temp4 = func.get_dataSet_access(sql_temp8, conn1).Tables[0];

        //KEEP PFT DATA 20130121
        if (Convert.ToInt16(dt_temp4.Rows[0][0].ToString()) >= 1)
        {
            sql_temp4 = @"

      drop table  {0}


";
            //pp_frequnce_table
            sql_temp4 = string.Format(sql_temp4, "PFT_" + line + area.Replace("-", "_") + eq+" purge");
            func.get_sql_execute(sql_temp4, conn1);
        }



        func.write_log("tacttime_calcular_PMD" + line + area + eq + " End", Server.MapPath(".") + "\\LOG\\", "log");

        GridView1.DataSource = ds_temp2.Tables[0];

        GridView1.DataBind();

        sql_temp5 = @"  
    select t.* from {0} t

";

        //pp_mode_data
        sql_temp5 = string.Format(sql_temp5, "PMD" + line + area.Replace("-", "_") + eq);

        dt_temp2 = func.get_dataSet_access(sql_temp5, conn1).Tables[0];


        DataTable dt_moduale = dt_temp2.DefaultView.ToTable("moduale", true);

        DataTable dt_equiptype = dt_temp2.DefaultView.ToTable("equiptype", true);

        DataTable dt_product_type = dt_temp2.DefaultView.ToTable("product_type", true);

        DataTable dt_layer = dt_temp2.DefaultView.ToTable("layer", true);
















    }


    private void show_data(string moduale, string equiptype)
    {



        string sql_temp6 = @"
select * from (

select t.moduale,
         case when t.equiptype like '0AEXP100' then 'EXP-C' 
            when t.equiptype like '0AEXP200' then 'EXP-C' 
            when t.equiptype like '0AEXP700' then 'EXP-C'
            when t.equiptype like '0AEXP800' then 'EXP-C' 
            when t.equiptype like '0AEXP900' then 'EXP-C' 
            when t.equiptype like '0AEXPA00' then 'EXP-C' 
            when t.equiptype like '0AEXP300' then 'EXP-N'
            when t.equiptype like '0AEXP300' then 'EXP-N'
            when t.equiptype like '0AEXP400' then 'EXP-N'
            when t.equiptype like '0AEXP500' then 'EXP-N'
            when t.equiptype like '0AEXP600' then 'EXP-N'
            when t.equiptype like '0AEXPB00' then 'EXP-S'
            when t.equiptype like '0AEXPC00' then 'EXP-S'
            else 
            substr(t.equiptype,0,5) end as equiptype,
            t.product_type,
            t.layer ,
            round(sum(t.counter1)/sum(t.counter) ,2) as tacttime,
             round(sum(t.counter)/0.95,0) as total_cnt
            
            
            from pp_mode_data_cal t



group by t.moduale,
         case when t.equiptype like '0AEXP100' then 'EXP-C' 
            when t.equiptype like '0AEXP200' then 'EXP-C' 
            when t.equiptype like '0AEXP700' then 'EXP-C'
            when t.equiptype like '0AEXP800' then 'EXP-C' 
            when t.equiptype like '0AEXP900' then 'EXP-C' 
            when t.equiptype like '0AEXPA00' then 'EXP-C' 
            when t.equiptype like '0AEXP300' then 'EXP-N'
            when t.equiptype like '0AEXP300' then 'EXP-N'
            when t.equiptype like '0AEXP400' then 'EXP-N'
            when t.equiptype like '0AEXP500' then 'EXP-N'
            when t.equiptype like '0AEXP600' then 'EXP-N'
            when t.equiptype like '0AEXPB00' then 'EXP-S'
            when t.equiptype like '0AEXPC00' then 'EXP-S'
            else 
            substr(t.equiptype,0,5) end ,
            t.product_type,
            t.layer
             
) ot2

order by case when ot2.moduale='TF' then 1
              when ot2.moduale='PH' then 2
              when ot2.moduale='TF/PH' then 3
              when ot2.moduale='ET' then 4
              else 5 
              end,
          ot2.equiptype,
          ot2.product_type,
          case
            when ot2.layer = '高溫ITO' then
             1
            when ot2.layer = 'GMET' then
             2
            when ot2.layer = '常溫ITO' then
             3
            when ot2.layer = 'METITO' then
             4
            else
             5
          end

";
        sql_temp6 = string.Format(sql_temp6, moduale, equiptype);

        ds_temp1 = func.get_dataSet_access(sql_temp6, conn1);

        table_col_num = ds_temp1.Tables[0].Columns.Count;
        table_row_num = ds_temp1.Tables[0].Rows.Count;
        //GridView1.DataSource = func.Table_transport1(ds_temp1.Tables[0]);

        GridView1.DataSource = ds_temp1.Tables[0];

        GridView1.DataBind();



    }


    private void calcular_mode_data_96(string pro_name, Double percentage_num, string line, string Area, string eq)  //source table calcular_mode_data get  percentage_num %
    {
        //T0ARRAY/0A-TF/0APVD    PMD table is still alive
        
        func.write_log("PP Tacttime  calcular_mode_data_" + pro_name + Convert.ToString(percentage_num) + " start ", Server.MapPath(".") + "\\LOG\\", "log");

        if (data_num >= 1)
        {




        }

        else

        { 
        
        
        }


        
        
        sql_temp5 = @"  
    select t.* from {0} t

";
        // PP_MODE_DATA
        sql_temp5 = string.Format(sql_temp5, "PMD" + line + Area.Replace("-", "_") + eq);

        dt_temp2 = func.get_dataSet_access(sql_temp5, conn1).Tables[0];

        for (int a = 0; a <= dt_temp2.Rows.Count-1; a++)
        {

            if (dt_temp2.Rows[a]["productid"].ToString().Equals(""))
            {

                dt_temp2.Rows[a]["productid"] = "NA";
            
            }

        }

        //DataTable dt_moduale = dt_temp2.DefaultView.ToTable(true, "moduale");

        //DataTable dt_equiptype = dt_temp2.DefaultView.ToTable(true, "equiptype");

        //DataTable dt_product_type = dt_temp2.DefaultView.ToTable(true, "product_type");

        //DataTable dt_layer = dt_temp2.DefaultView.ToTable(true, "layer");
        //DataTable dt_productid = dt_temp2.DefaultView.ToTable(true, "productid");

        //DataTable dt_dist = dt_temp2.DefaultView.ToTable(true, "moduale", "equiptype", "product_type", "layer", "productid");
        DataTable dt_dist = dt_temp2.DefaultView.ToTable(true, "moduale", "equiptype", "product_type", "layer");



        // Temp Table
        DataTable dt_get_percentage = new DataTable();
        dt_get_percentage.Columns.Add("moduale", typeof(string));
        dt_get_percentage.Columns.Add("equiptype", typeof(string));
        dt_get_percentage.Columns.Add("layer", typeof(string));
        dt_get_percentage.Columns.Add("endtacttime1", typeof(string));
        dt_get_percentage.Columns.Add("counter", typeof(string));
        dt_get_percentage.Columns.Add("counter1", typeof(string));
        dt_get_percentage.Columns.Add("product_type", typeof(string));
        dt_get_percentage.Columns.Add("per", typeof(string));
        dt_get_percentage.Columns.Add("productid", typeof(string));

        // Temp Table don't have product id
        DataTable dt_get_percentage_np = new DataTable();
        dt_get_percentage_np.Columns.Add("moduale", typeof(string));
        dt_get_percentage_np.Columns.Add("equiptype", typeof(string));
        dt_get_percentage_np.Columns.Add("layer", typeof(string));
        dt_get_percentage_np.Columns.Add("endtacttime1", typeof(string));
        dt_get_percentage_np.Columns.Add("counter", typeof(string));
        dt_get_percentage_np.Columns.Add("counter1", typeof(string));
        dt_get_percentage_np.Columns.Add("product_type", typeof(string));
        dt_get_percentage_np.Columns.Add("per", typeof(string));
      



        for (int z = 0; z <= dt_dist.Rows.Count-1; z++)
        {

//            sql_temp = @" 
//select t.moduale,
//       t.equiptype,
//       t.product_type,
//       t.layer,
//       t.productid,
//       t.endtacttime1,
//       t.counter,
//       t.counter1,
//       t.counter / (select sum(tt.counter)
//                      from {4} tt
//                     where tt.moduale = '{0}'
//                       and tt.equiptype = '{1}'
//                       and tt.product_type = '{2}'
//                       and tt.layer = '{3}') as per
//
//  from {4} t
// where t.moduale = '{0}'
//   and t.equiptype = '{1}'
//   and t.product_type = '{2}'
//   and t.layer = '{3}'
//   and t.productid='{5}'
//   order by t.counter desc
//
//";


            sql_temp = @" 
select t.moduale,
       t.equiptype,
       t.product_type,
       t.layer,
       t.productid,
       t.endtacttime1,
       t.counter,
       t.counter1,
       t.counter / (select sum(tt.counter)
                      from {4} tt
                     where tt.moduale = '{0}'
                       and tt.equiptype = '{1}'
                       and tt.product_type = '{2}'
                       and tt.layer = '{3}') as per

  from {4} t
 where t.moduale = '{0}'
   and t.equiptype = '{1}'
   and t.product_type = '{2}'
   and t.layer = '{3}'
  
   order by t.counter desc,to_number(endtacttime1) asc

";

            // pp_mode_data  Do not Filter product id
            sql_temp = string.Format(sql_temp, dt_dist.Rows[z][0].ToString(), dt_dist.Rows[z][1].ToString(), dt_dist.Rows[z][2].ToString(), dt_dist.Rows[z][3].ToString(), "PMD" + line + Area.Replace("-", "_") + eq);


            // have product id

            dt_temp2 = func.get_dataSet_access(sql_temp, conn1).Tables[0];


            // Don't have product id 

            sql_temp = @" 

select ot2.*,
 ot2.counter / (select sum(tt.counter)
                      from {4} tt
                     where tt.moduale = '{0}'
                       and tt.equiptype = '{1}'
                       and tt.product_type = '{2}'
                       and tt.layer = '{3}') as per


 from (

select ot1.moduale,
       ot1.equiptype,
       ot1.product_type,
       ot1.layer,
       ot1.endtacttime1,
       sum(ot1.counter) as counter,
       sum(ot1.counter1) as counter1,
       sysdate as dttm
  from (select t.*, t.rowid
          from {4} t
         where t.moduale = '{0}'
           and t.equiptype = '{1}'
           and t.product_type = '{2}'
           and t.layer = '{3}'
           ) ot1

 group by ot1.moduale,
          ot1.equiptype,
          ot1.product_type,
          ot1.layer,
          ot1.endtacttime1

 order by counter desc,to_number(endtacttime1) asc 
 
) ot2






";

            // pp_mode_data  Don't have product id
            sql_temp = string.Format(sql_temp, dt_dist.Rows[z][0].ToString(), dt_dist.Rows[z][1].ToString(), dt_dist.Rows[z][2].ToString(), dt_dist.Rows[z][3].ToString(), "PMD" + line + Area.Replace("-", "_") + eq);


            // have product id
         
            dt_temp3 = func.get_dataSet_access(sql_temp, conn1).Tables[0];

            


            Double calculate95 = 0;

            Double calculate95_np = 0;


            // collection satisfication 95%  Data 
            for (int q = 0; q <= dt_temp2.Rows.Count - 1; q++)
            {





                if (calculate95 < percentage_num)     // 201304241741  modified by oscar just 95%, Dont count next data
                //if (calculate95 <= percentage_num)
                {

                    DataRow dRow = dt_get_percentage.NewRow();

                    dRow["moduale"] = dt_temp2.Rows[q]["moduale"];
                    dRow["equiptype"] = dt_temp2.Rows[q]["equiptype"];
                    dRow["layer"] = dt_temp2.Rows[q]["layer"];
                    dRow["endtacttime1"] = dt_temp2.Rows[q]["endtacttime1"];
                    dRow["counter"] = dt_temp2.Rows[q]["counter"];
                    dRow["counter1"] = dt_temp2.Rows[q]["counter1"];
                    dRow["product_type"] = dt_temp2.Rows[q]["product_type"];
                    dRow["per"] = dt_temp2.Rows[q]["per"];
                    dRow["productid"] = dt_temp2.Rows[q]["productid"];



                    dt_get_percentage.Rows.Add(dRow);




                }
                else
                {

                }

                calculate95 += Convert.ToDouble(dt_temp2.Rows[q]["per"].ToString());



            }




            for (int p = 0; p <= dt_get_percentage.Rows.Count - 1; p++)
            {

                sql_temp3 = @"

insert into pp_mode_data_cal
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm,productid)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate,'{7}')


";


                // pp_mode_data_cal 
                sql_temp3 = string.Format(sql_temp3, dt_get_percentage.Rows[p]["moduale"].ToString(), dt_get_percentage.Rows[p]["equiptype"].ToString(), dt_get_percentage.Rows[p]["product_type"].ToString(), dt_get_percentage.Rows[p]["layer"].ToString(), dt_get_percentage.Rows[p]["endtacttime1"].ToString(), dt_get_percentage.Rows[p]["counter"].ToString(), dt_get_percentage.Rows[p]["counter1"].ToString(), dt_get_percentage.Rows[p]["productid"].ToString());


                func.get_sql_execute(sql_temp3, conn1);


            }
            dt_get_percentage.Clear();

            // collection satisfication 95%  Data Don't have product id
            for (int q = 0; q <= dt_temp3.Rows.Count - 1; q++)
            {





                if (calculate95_np < percentage_num)        // 201304251635 modified by oscar just 95%, Dont count next data
                //if (calculate95_np <= percentage_num)
                {

                    DataRow dRow = dt_get_percentage_np.NewRow();

                    dRow["moduale"] = dt_temp3.Rows[q]["moduale"];
                    dRow["equiptype"] = dt_temp3.Rows[q]["equiptype"];
                    dRow["layer"] = dt_temp3.Rows[q]["layer"];
                    dRow["endtacttime1"] = dt_temp3.Rows[q]["endtacttime1"];
                    dRow["counter"] = dt_temp3.Rows[q]["counter"];
                    dRow["counter1"] = dt_temp3.Rows[q]["counter1"];
                    dRow["product_type"] = dt_temp3.Rows[q]["product_type"];
                    dRow["per"] = dt_temp3.Rows[q]["per"];




                    dt_get_percentage_np.Rows.Add(dRow);




                }
                else
                {

                }

                calculate95_np += Convert.ToDouble(dt_temp3.Rows[q]["per"].ToString());



            }



            //oscar 20121204


            for (int p = 0; p <= dt_get_percentage_np.Rows.Count - 1; p++)
            {

                sql_temp3 = @"

insert into pp_mode_data_cal_np
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate)


";


                // pp_mode_data_cal 
                sql_temp3 = string.Format(sql_temp3, dt_get_percentage_np.Rows[p]["moduale"].ToString(), dt_get_percentage_np.Rows[p]["equiptype"].ToString(), dt_get_percentage_np.Rows[p]["product_type"].ToString(), dt_get_percentage_np.Rows[p]["layer"].ToString(), dt_get_percentage_np.Rows[p]["endtacttime1"].ToString(), dt_get_percentage_np.Rows[p]["counter"].ToString(), dt_get_percentage_np.Rows[p]["counter1"].ToString());


                func.get_sql_execute(sql_temp3, conn1);


            }

         
            dt_get_percentage_np.Clear();

        }
















        dt_get_percentage.Dispose();
        dt_get_percentage_np.Dispose();


        sql_temp8 = @"
        select count(t.owner) as counter from all_tables t
 where t.table_name=upper('{0}')
      
";
        sql_temp8 = string.Format(sql_temp8, "PMD" + line + Area.Replace("-", "_") + eq);

        dt_temp4 = func.get_dataSet_access(sql_temp8, conn1).Tables[0];

        if (Convert.ToInt16(dt_temp4.Rows[0][0].ToString()) >= 1)
        {
            sql_temp4 = @"

      drop table  {0}


";
            // pp_mode_data
            sql_temp4 = string.Format(sql_temp4, "PMD" + line + Area.Replace("-", "_") + eq+" purge");

            func.get_sql_execute(sql_temp4, conn1);

        


        }





        func.write_log("PP Tacttime  calcular_mode_data_" + pro_name + Convert.ToString(percentage_num) + " End ", Server.MapPath(".") + "\\LOG\\", "log");


    }


    private void calcular_mode_data_95(string pro_name, Double percentage_num, string line, string Area, string eq)  //source table calcular_mode_data get  percentage_num %
    {
        //T0ARRAY/0A-TF/0APVD    PMD table is still alive

        func.write_log("PP Tacttime  calcular_mode_data_" + pro_name + Convert.ToString(percentage_num) + " start ", Server.MapPath(".") + "\\LOG\\", "log");

        if (data_num == 0)
        {

            return;


        }

        else
        {

            sql_temp5 = @"  
    select t.* from {0} t

";
            // PP_MODE_DATA
            sql_temp5 = string.Format(sql_temp5, "PMD" + line + Area.Replace("-", "_") + eq);

            dt_temp2 = func.get_dataSet_access(sql_temp5, conn1).Tables[0];

            for (int a = 0; a <= dt_temp2.Rows.Count - 1; a++)
            {

                if (dt_temp2.Rows[a]["productid"].ToString().Equals(""))
                {

                    dt_temp2.Rows[a]["productid"] = "NA";

                }

            }

            //DataTable dt_moduale = dt_temp2.DefaultView.ToTable(true, "moduale");

            //DataTable dt_equiptype = dt_temp2.DefaultView.ToTable(true, "equiptype");

            //DataTable dt_product_type = dt_temp2.DefaultView.ToTable(true, "product_type");

            //DataTable dt_layer = dt_temp2.DefaultView.ToTable(true, "layer");
            //DataTable dt_productid = dt_temp2.DefaultView.ToTable(true, "productid");

            //DataTable dt_dist = dt_temp2.DefaultView.ToTable(true, "moduale", "equiptype", "product_type", "layer", "productid");
            DataTable dt_dist = dt_temp2.DefaultView.ToTable(true, "moduale", "equiptype", "product_type", "layer");



            // Temp Table
            DataTable dt_get_percentage = new DataTable();
            dt_get_percentage.Columns.Add("moduale", typeof(string));
            dt_get_percentage.Columns.Add("equiptype", typeof(string));
            dt_get_percentage.Columns.Add("layer", typeof(string));
            dt_get_percentage.Columns.Add("endtacttime1", typeof(string));
            dt_get_percentage.Columns.Add("counter", typeof(string));
            dt_get_percentage.Columns.Add("counter1", typeof(string));
            dt_get_percentage.Columns.Add("product_type", typeof(string));
            dt_get_percentage.Columns.Add("per", typeof(string));
            dt_get_percentage.Columns.Add("productid", typeof(string));

            // Temp Table don't have product id
            DataTable dt_get_percentage_np = new DataTable();
            dt_get_percentage_np.Columns.Add("moduale", typeof(string));
            dt_get_percentage_np.Columns.Add("equiptype", typeof(string));
            dt_get_percentage_np.Columns.Add("layer", typeof(string));
            dt_get_percentage_np.Columns.Add("endtacttime1", typeof(string));
            dt_get_percentage_np.Columns.Add("counter", typeof(string));
            dt_get_percentage_np.Columns.Add("counter1", typeof(string));
            dt_get_percentage_np.Columns.Add("product_type", typeof(string));
            dt_get_percentage_np.Columns.Add("per", typeof(string));




            for (int z = 0; z <= dt_dist.Rows.Count - 1; z++)
            {

                //            sql_temp = @" 
                //select t.moduale,
                //       t.equiptype,
                //       t.product_type,
                //       t.layer,
                //       t.productid,
                //       t.endtacttime1,
                //       t.counter,
                //       t.counter1,
                //       t.counter / (select sum(tt.counter)
                //                      from {4} tt
                //                     where tt.moduale = '{0}'
                //                       and tt.equiptype = '{1}'
                //                       and tt.product_type = '{2}'
                //                       and tt.layer = '{3}') as per
                //
                //  from {4} t
                // where t.moduale = '{0}'
                //   and t.equiptype = '{1}'
                //   and t.product_type = '{2}'
                //   and t.layer = '{3}'
                //   and t.productid='{5}'
                //   order by t.counter desc
                //
                //";


                sql_temp = @" 
select t.moduale,
       t.equiptype,
       t.product_type,
       t.layer,
       t.productid,
       t.endtacttime1,
       t.counter,
       t.counter1,
       t.counter / (select sum(tt.counter)
                      from {4} tt
                     where tt.moduale = '{0}'
                       and tt.equiptype = '{1}'
                       and tt.product_type = '{2}'
                       and tt.layer = '{3}') as per

  from {4} t
 where t.moduale = '{0}'
   and t.equiptype = '{1}'
   and t.product_type = '{2}'
   and t.layer = '{3}'
  
   order by t.counter desc,to_number(endtacttime1) asc

";

                // pp_mode_data  Do not Filter product id
                sql_temp = string.Format(sql_temp, dt_dist.Rows[z][0].ToString(), dt_dist.Rows[z][1].ToString(), dt_dist.Rows[z][2].ToString(), dt_dist.Rows[z][3].ToString(), "PMD" + line + Area.Replace("-", "_") + eq);


                // have product id

                dt_temp2 = func.get_dataSet_access(sql_temp, conn1).Tables[0];


                // Don't have product id 

                sql_temp = @" 

select ot2.*,
 ot2.counter / (select sum(tt.counter)
                      from {4} tt
                     where tt.moduale = '{0}'
                       and tt.equiptype = '{1}'
                       and tt.product_type = '{2}'
                       and tt.layer = '{3}') as per


 from (

select ot1.moduale,
       ot1.equiptype,
       ot1.product_type,
       ot1.layer,
       ot1.endtacttime1,
       sum(ot1.counter) as counter,
       sum(ot1.counter1) as counter1,
       sysdate as dttm
  from (select t.*, t.rowid
          from {4} t
         where t.moduale = '{0}'
           and t.equiptype = '{1}'
           and t.product_type = '{2}'
           and t.layer = '{3}'
           ) ot1

 group by ot1.moduale,
          ot1.equiptype,
          ot1.product_type,
          ot1.layer,
          ot1.endtacttime1

 order by counter desc,to_number(endtacttime1) asc 
 
) ot2






";

                // pp_mode_data  Don't have product id
                sql_temp = string.Format(sql_temp, dt_dist.Rows[z][0].ToString(), dt_dist.Rows[z][1].ToString(), dt_dist.Rows[z][2].ToString(), dt_dist.Rows[z][3].ToString(), "PMD" + line + Area.Replace("-", "_") + eq);


                // have product id

                dt_temp3 = func.get_dataSet_access(sql_temp, conn1).Tables[0];




                Double calculate95 = 0;

                Double calculate95_np = 0;


                // collection satisfication 95%  Data 
                for (int q = 0; q <= dt_temp2.Rows.Count - 1; q++)
                {





                    if (calculate95 < percentage_num)     // 201304241741  modified by oscar just 95%, Dont count next data
                    //if (calculate95 <= percentage_num)
                    {

                        DataRow dRow = dt_get_percentage.NewRow();

                        dRow["moduale"] = dt_temp2.Rows[q]["moduale"];
                        dRow["equiptype"] = dt_temp2.Rows[q]["equiptype"];
                        dRow["layer"] = dt_temp2.Rows[q]["layer"];
                        dRow["endtacttime1"] = dt_temp2.Rows[q]["endtacttime1"];
                        dRow["counter"] = dt_temp2.Rows[q]["counter"];
                        dRow["counter1"] = dt_temp2.Rows[q]["counter1"];
                        dRow["product_type"] = dt_temp2.Rows[q]["product_type"];
                        dRow["per"] = dt_temp2.Rows[q]["per"];
                        dRow["productid"] = dt_temp2.Rows[q]["productid"];



                        dt_get_percentage.Rows.Add(dRow);




                    }
                    else
                    {

                    }

                    calculate95 += Convert.ToDouble(dt_temp2.Rows[q]["per"].ToString());



                }




                for (int p = 0; p <= dt_get_percentage.Rows.Count - 1; p++)
                {

                    sql_temp3 = @"

insert into pp_mode_data_cal
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm,productid)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate,'{7}')


";


                    // pp_mode_data_cal 
                    sql_temp3 = string.Format(sql_temp3, dt_get_percentage.Rows[p]["moduale"].ToString(), dt_get_percentage.Rows[p]["equiptype"].ToString(), dt_get_percentage.Rows[p]["product_type"].ToString(), dt_get_percentage.Rows[p]["layer"].ToString(), dt_get_percentage.Rows[p]["endtacttime1"].ToString(), dt_get_percentage.Rows[p]["counter"].ToString(), dt_get_percentage.Rows[p]["counter1"].ToString(), dt_get_percentage.Rows[p]["productid"].ToString());


                    func.get_sql_execute(sql_temp3, conn1);


                }
                dt_get_percentage.Clear();

                // collection satisfication 95%  Data Don't have product id
                for (int q = 0; q <= dt_temp3.Rows.Count - 1; q++)
                {





                    if (calculate95_np < percentage_num)        // 201304251635 modified by oscar just 95%, Dont count next data
                    //if (calculate95_np <= percentage_num)
                    {

                        DataRow dRow = dt_get_percentage_np.NewRow();

                        dRow["moduale"] = dt_temp3.Rows[q]["moduale"];
                        dRow["equiptype"] = dt_temp3.Rows[q]["equiptype"];
                        dRow["layer"] = dt_temp3.Rows[q]["layer"];
                        dRow["endtacttime1"] = dt_temp3.Rows[q]["endtacttime1"];
                        dRow["counter"] = dt_temp3.Rows[q]["counter"];
                        dRow["counter1"] = dt_temp3.Rows[q]["counter1"];
                        dRow["product_type"] = dt_temp3.Rows[q]["product_type"];
                        dRow["per"] = dt_temp3.Rows[q]["per"];




                        dt_get_percentage_np.Rows.Add(dRow);




                    }
                    else
                    {

                    }

                    calculate95_np += Convert.ToDouble(dt_temp3.Rows[q]["per"].ToString());



                }



                //oscar 20121204


                for (int p = 0; p <= dt_get_percentage_np.Rows.Count - 1; p++)
                {

                    sql_temp3 = @"

insert into pp_mode_data_cal_np
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate)


";


                    // pp_mode_data_cal 
                    sql_temp3 = string.Format(sql_temp3, dt_get_percentage_np.Rows[p]["moduale"].ToString(), dt_get_percentage_np.Rows[p]["equiptype"].ToString(), dt_get_percentage_np.Rows[p]["product_type"].ToString(), dt_get_percentage_np.Rows[p]["layer"].ToString(), dt_get_percentage_np.Rows[p]["endtacttime1"].ToString(), dt_get_percentage_np.Rows[p]["counter"].ToString(), dt_get_percentage_np.Rows[p]["counter1"].ToString());


                    func.get_sql_execute(sql_temp3, conn1);


                }


                dt_get_percentage_np.Clear();

            }
















            dt_get_percentage.Dispose();
            dt_get_percentage_np.Dispose();


            sql_temp8 = @"
        select count(t.owner) as counter from all_tables t
 where t.table_name=upper('{0}')
      
";
            sql_temp8 = string.Format(sql_temp8, "PMD" + line + Area.Replace("-", "_") + eq);

            dt_temp4 = func.get_dataSet_access(sql_temp8, conn1).Tables[0];

            if (Convert.ToInt16(dt_temp4.Rows[0][0].ToString()) >= 1)
            {
                sql_temp4 = @"

      drop table  {0}


";
                // pp_mode_data
                sql_temp4 = string.Format(sql_temp4, "PMD" + line + Area.Replace("-", "_") + eq + " purge");

                func.get_sql_execute(sql_temp4, conn1);




            }

        }









        func.write_log("PP Tacttime  calcular_mode_data_" + pro_name + Convert.ToString(percentage_num) + " End ", Server.MapPath(".") + "\\LOG\\", "log");


    }

    private void calcular_mode_data_position(Int32 row_num)  //source table calcular_mode_data  3 position
    {

        func.write_log("PP Tacttime  calcular_mode_data_position Start ", Server.MapPath(".") + "\\LOG\\", "log");


        sql_temp5 = @"  
    select t.* from pp_mode_data t

";
        dt_temp2 = func.get_dataSet_access(sql_temp5, conn1).Tables[0];


        DataTable dt_moduale = dt_temp2.DefaultView.ToTable(true, "moduale");

        DataTable dt_equiptype = dt_temp2.DefaultView.ToTable(true, "equiptype");

        DataTable dt_product_type = dt_temp2.DefaultView.ToTable(true, "product_type");

        DataTable dt_layer = dt_temp2.DefaultView.ToTable(true, "layer");


        for (int i = 0; i <= dt_moduale.Rows.Count - 1; i++)
        {

            for (int j = 0; j <= dt_equiptype.Rows.Count - 1; j++)
            {

                for (int k = 0; k <= dt_product_type.Rows.Count - 1; k++)
                {


                    for (int m = 0; m <= dt_layer.Rows.Count - 1; m++)
                    {


                        sql_temp = @" 
select t.* from pp_mode_data t
where t.moduale='{0}' and t.equiptype='{1}' and t.product_type='{2}' and t.layer='{3}'
and rownum<={4}
";

                        sql_temp = string.Format(sql_temp, dt_moduale.Rows[i][0].ToString(), dt_equiptype.Rows[j][0].ToString(), dt_product_type.Rows[k][0].ToString(), dt_layer.Rows[m][0].ToString(), row_num);

                        ds_temp3 = func.get_dataSet_access(sql_temp, conn1);

                        for (int p = 0; p <= ds_temp3.Tables[0].Rows.Count - 1; p++)
                        {

                            sql_temp3 = @"

insert into pp_mode_data_cal
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate)


";

                            sql_temp3 = string.Format(sql_temp3, ds_temp3.Tables[0].Rows[p]["moduale"].ToString(), ds_temp3.Tables[0].Rows[p]["equiptype"].ToString(), ds_temp3.Tables[0].Rows[p]["product_type"].ToString(), ds_temp3.Tables[0].Rows[p]["layer"].ToString(), ds_temp3.Tables[0].Rows[p]["endtacttime1"].ToString(), ds_temp3.Tables[0].Rows[p]["counter"].ToString(), ds_temp3.Tables[0].Rows[p]["counter1"].ToString());


                            func.get_sql_execute(sql_temp3, conn1);


                        }


                        ds_temp3.Clear();





                    }


                }


            }

        }


        sql_temp4 = @"

      truncate table  pp_mode_data


";

        func.get_sql_execute(sql_temp4, conn1);

        func.write_log("PP Tacttime  calcular_mode_data_position End ", Server.MapPath(".") + "\\LOG\\", "log");

    }


}
