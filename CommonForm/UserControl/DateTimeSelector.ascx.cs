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

public partial class Fabinfo_CF_Report_CF_Defect_Trend_DateTimeSelector : System.Web.UI.UserControl
{
    #region Members
    public string StartDateTime
    {
        get 
        {
            switch (this.DateTimeType)
            { 
                case 0:
					return this.hourlyPicker.StartDateTime;
                case 1:
					return this.dailyPicker.StartDateTime;
                case 2:
                    return this.weeklyPicker.StartDateTime;
                default:
                    return this.monthPicker.StartDateTime;
            }
        }
        set
        {
            switch (this.DateTimeType)
            {
                case 0:
                    this.hourlyPicker.StartDateTime = value;
                    break;
                case 1:
                    //this.dailyPicker.StartDateTime = value;
                    break;
                case 2:
                    //this.weeklyPicker.StartDateTime = value;
                    break;
                default:
                    //this.monthPicker.StartDateTime = value;
                    break;
            }
        }
    }

	public string ShiftStartDateTime
	{
		get
		{
			switch (this.DateTimeType)
			{
				case 0:
					return this.hourlyPicker.StartDateTime;
				case 1:
					return this.dailyPicker.StartDateTime;
				case 2:
					return this.weeklyPicker.ShiftStartDateTime;
				default:
					return this.monthPicker.StartDateTime;
			}
		}
	}

    public string EndDateTime
    {
        get {
            switch (this.DateTimeType)
            {
                case 0:
					return this.hourlyPicker.EndDateTime;
                case 1:
					return this.dailyPicker.EndDateTime;
                case 2:
					return this.weeklyPicker.EndDateTime;
                default:
					return this.monthPicker.EndDateTime;
            }
        }
        set
        {
            switch (this.DateTimeType)
            {
                case 0:
                    this.hourlyPicker.EndDateTime = value;
                    break;
                case 1:
                    //this.dailyPicker.EndDateTime = value;
                    break;
                case 2:
                    //this.weeklyPicker.EndDateTime = value;
                    break;
                default:
                    //this.monthPicker.EndDateTime = value;
                    break;
            }
        }
    }

	public string ShiftEndDateTime
	{
		get
		{
			switch (this.DateTimeType)
			{
				case 0:
					return this.hourlyPicker.EndDateTime;
				case 1:
					return this.dailyPicker.EndDateTime;
				case 2:
					return this.weeklyPicker.ShiftEndDateTime;
				default:
					return this.monthPicker.EndDateTime;
			}
		}
	}

    // Todo: 
    public bool DisplayHourly
    {
        get 
        {
            if (ViewState["DisplayHourly"] == null)
                return true;
            return bool.Parse(ViewState["DisplayHourly"].ToString());
        }
        set { ViewState["DisplayHourly"] = value; }
    }
    public bool DisplayDaily
    {
        get
        {
            if (ViewState["DisplayDaily"] == null)
                return true;
            return bool.Parse(ViewState["DisplayDaily"].ToString());
        }
        set { ViewState["DisplayDaily"] = value; }
    }
    public bool DisplayWeekly
    {
        get
        {
            if (ViewState["DisplayWeekly"] == null)
                return true;
            return bool.Parse(ViewState["DisplayWeekly"].ToString());
        }
        set { ViewState["DisplayWeekly"] = value; }
    }
    public bool DisplayMonthly
    {
        get
        {
            if (ViewState["DisplayMonthly"] == null)
                return true;
            return bool.Parse(ViewState["DisplayMonthly"].ToString());
        }
        set { ViewState["DisplayMonthly"] = value; }
    }

    public int DateTimeType
    {
        get { return this.TimeTypeMultiPage.SelectedIndex; }
        set 
        { 
            this.TimeTypeTabStrip.SelectedIndex = this.TimeTypeMultiPage.SelectedIndex = value;
        }
    }

    public string DateTimeTypeString
    {
        get
        {
            switch (this.DateTimeType)
            {
                case 0:
                    return "H";
                case 1:
                    return "D";
                case 2:
                    return "W";
                default:
                    return "M";
            }
        }
    }

    public double Width
    {
        get
        {
            if (ViewState["Width"] == null || string.IsNullOrEmpty(ViewState["Width"].ToString()))
                return 250;
            return double.Parse(ViewState["Width"].ToString());
        }
        set { ViewState["Width"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
			this.TimeTypeTabStrip.Tabs[0].Visible = this.HourPageView.Visible = this.DisplayHourly;
            this.TimeTypeTabStrip.Tabs[1].Visible = this.DayPageView.Visible = this.DisplayDaily;
            this.TimeTypeTabStrip.Tabs[2].Visible = this.WeekPageView.Visible = this.DisplayWeekly;
            this.TimeTypeTabStrip.Tabs[3].Visible = this.MonthPageView.Visible = this.DisplayMonthly;
            if (this.DisplayDaily == false)
                this.TimeTypeTabStrip.SelectedIndex = this.TimeTypeMultiPage.SelectedIndex = 0;

            this.TimeTypeTabStrip.Width = new Unit(this.Width);
        }
    }
}
