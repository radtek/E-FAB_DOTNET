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

public partial class CommonForm_UserControl_Scheduler_MinutelySetting : System.Web.UI.UserControl
{
	public Trigger GetTrigger()
	{
		Trigger trigger = TriggerUtils.MakeMinutelyTrigger(int.Parse(this.txtIntervalMinute.Text), int.Parse(this.txtRepeatCount.Text));

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
		return trigger;
	}

	protected void Page_Load(object sender, EventArgs e)
	{

	}
}
