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
using System.Data.OleDb;

public partial class CF_load_cf_daily_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
string strConn; 
strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + 
"Data Source=D:\\CIM-SE-RPT-WEB\\E-FAB_dotnet\\CF\\T1CF_DAILY_20110616072002.xls;" + 
"Extended Properties=Excel 8.0;";

OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet4$]", strConn); 
 
DataSet myDataSet = new DataSet(); 
myCommand.Fill(myDataSet, "ExcelInfo"); 
this.GridView1.DataSource = myDataSet.Tables["ExcelInfo"].DefaultView; 
this.GridView1.DataBind(); 
    }
}
