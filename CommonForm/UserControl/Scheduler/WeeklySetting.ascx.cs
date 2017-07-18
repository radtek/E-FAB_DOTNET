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
using Quartz;
using Innolux.Portal.Common.Report.Scheduler;

public partial class CommonForm_UserControl_Scheduler_WeeklySetting : System.Web.UI.UserControl
{
    public Trigger GetTrigger()
    {
        if (string.IsNullOrEmpty(this.txtTimeOfDay.Text))
        {
            throw new ApplicationException("Please set the time of day.");
        }
        string[] temp = this.txtTimeOfDay.Text.Split(':');
        Trigger trigger = TriggerUtils.MakeWeeklyTrigger(this.SelectedDayOfWeek, int.Parse(temp[0]), int.Parse(temp[1]));

        if (!string.IsNullOrEmpty(this.txtStartTime.Text))
        {
            DateTime startDateTime;
            if (DateTime.TryParse(this.txtStartTime.Text, out startDateTime))
            {
                trigger.StartTimeUtc = startDateTime;
            }
        }

        if (!string.IsNullOrEmpty(this.txtEndTime.Text))
        {
            DateTime endDateTime;
            if (DateTime.TryParse(this.txtEndTime.Text, out endDateTime))
            {
                trigger.EndTimeUtc = endDateTime;
            }
        }
        trigger.MisfireInstruction = MisfireInstruction.CronTrigger.DoNothing;
        return trigger;
    }

    private DayOfWeek SelectedDayOfWeek
    {
        get
        {
            switch (this.ddlDayOfWeek.SelectedValue)
            { 
                case "0":
                    return DayOfWeek.Sunday;
                case "1":
                    return DayOfWeek.Monday;
                case "2":
                    return DayOfWeek.Tuesday;
                case "3":
                    return DayOfWeek.Wednesday;
                case "4":
                    return DayOfWeek.Thursday;
                case "5":
                    return DayOfWeek.Friday;
                case "6":
                    return DayOfWeek.Sunday;
                default:
                    return DayOfWeek.Sunday;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
