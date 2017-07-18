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

public partial class CommonForm_UserControl_DateTimePicker_HourlyPicker : System.Web.UI.UserControl
{
	public int MaxDuration
	{
		get
		{
			object obj = this.ViewState["MaxHourlyDuration"];
			return obj == null ? -1 : (int)obj;
		}
		set { this.ViewState["MaxHourlyDuration"] = value; }
	}

	public string StartDateTime
	{
		get
		{
			return StartHourPicker.SelectedDate.Value.ToString("yyyyMMddHH");
		}
        set
        {
            ViewState["StartDateTime"] = DateTime.Parse(value);
            this.StartHourPicker.SelectedDate = DateTime.Parse(value);
        }
	}

	public string EndDateTime
	{
		get
		{
			if (StartHourPicker.SelectedDate.Value > EndHourPicker.SelectedDate.Value)
			{
				throw new ApplicationException("選擇的開始時間不能大于結束時間，請重新選擇！");
			}
			else if (EndHourPicker.SelectedDate.Value > StartHourPicker.SelectedDate.Value.AddDays(this.MaxDuration))
			{
                throw new ApplicationException("選擇的時間區間大于" + this.MaxDuration + "天，請重新選擇！");
			}
			return EndHourPicker.SelectedDate.Value.ToString("yyyyMMddHH");
		}
        set 
        {
            ViewState["EndDateTime"] = DateTime.Parse(value);
            this.EndHourPicker.SelectedDate = DateTime.Parse(value); 
        }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            if (ViewState["StartDateTime"] == null || ViewState["EndDateTime"] == null)
            {
                DateTime currentTime = DateTime.Now;
                //hour        
                StartHourPicker.SelectedDate = currentTime.Date.AddHours(7);
                EndHourPicker.SelectedDate = currentTime;
            }
            else
            {
                StartHourPicker.SelectedDate = DateTime.Parse(ViewState["StartDateTime"].ToString());
                EndHourPicker.SelectedDate = DateTime.Parse(ViewState["EndDateTime"].ToString()); 
            }
		}
	}

    public void SetDefault(DateTime startDateTime, DateTime endDateTime)
    {
        this.StartHourPicker.SelectedDate = startDateTime;
        this.EndHourPicker.SelectedDate = endDateTime;
    }
}
