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
using System.Drawing;

public partial class calendar_AddControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void addControl(DayRenderEventArgs e, string project_id, string project_name, string status, DateTime estimate_start_dttm, DateTime estimate_end_dttm, string navigateUrl)
    {
        string strStartText = "<div align='left' style='padding:2px'>";
        string strEndText = "</div>";

        if ((status != "Close" && status != "Pending") && estimate_end_dttm < DateTime.Now)
            status = "Delay";

        //特定日期，加入專案名稱
        if (e.Day.Date >= estimate_start_dttm && e.Day.Date <= estimate_end_dttm)
        {
            HyperLink link = new HyperLink();

            string strNavigateUrl = navigateUrl + project_id;
            string statusColor = getStatusColor(status).Name;
            string strToolTip = "開啟[" + project_name + "]檢視";

            project_name = "<span style='background-color:" + statusColor + ";'>" + project_name + "</span>";

            if (e.Day.Date == estimate_start_dttm)
                strStartText += "<span style='font-weight:bold; font-size:15px; color:red'>{</span>";
            if (e.Day.Date == estimate_end_dttm)
                strEndText = "<span style='font-weight:bold; font-size:15px; color:red'>}</span>" + strEndText;

            link.Text = strStartText + project_name + strEndText;
            link.Target = "_blank";
            link.NavigateUrl = strNavigateUrl;
            link.ToolTip = strToolTip;

            e.Cell.Controls.Add(link);
        }

        //最近一週Highlight顏色
        if (e.Day.Date >= DateTime.Now && e.Day.Date < DateTime.Now.AddDays(7))
        {
            e.Cell.BackColor = System.Drawing.Color.Azure;
        }
    }

    public Color getStatusColor(string status)
    {
        switch (status)
        {
            case "Receiving":
                return Color.SpringGreen;
                break;
            case "Processing":
                return Color.LightSkyBlue;
                break;
            case "Close":
                return Color.Gray;
                break;
            case "Pending":
                return Color.Yellow;
                break;
            case "Delay":
                return Color.Red;
                break;
            default:
                return Color.LawnGreen;
                break;
        }
    }

    public void setColorList(BulletedList blList)
    {
        string[] listStatus = { "Receiving", "Processing", "Close", "Pending", "Delay" };
        blList.Items.Clear();
        foreach (string status in listStatus)
        {
            ListItem listItem = new ListItem();
            listItem.Text = status;
            listItem.Attributes.Add("style", "background-color:" + getStatusColor(status).Name);
            blList.Items.Add(listItem);
        }

    }
}
