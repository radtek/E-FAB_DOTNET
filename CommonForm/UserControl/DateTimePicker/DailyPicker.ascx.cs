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
using Innolux.Portal.EntLibBlock;
using Innolux.Portal.EntLibBlock.DataAccess;

public partial class CommonForm_UserControl_DateTimePicker_DailyPicker : System.Web.UI.UserControl
{
	public int MaxDuration
	{
		get
		{
			object obj = this.ViewState["MaxDailyDuration"];
			return obj == null ? -1 : (int)obj;
		}
		set { this.ViewState["MaxDailyDuration"] = value; }
	}

	public string StartDateTime
	{
		get
		{
			return StartDatePicker.SelectedDate.Value.ToString("yyyyMMdd");
		}
	}

	public string EndDateTime
	{
		get
		{
			if (StartDatePicker.SelectedDate.Value > EndDatePicker.SelectedDate.Value)
			{
				throw new ApplicationException("選擇的開始時間不能大于結束時間，請重新選擇！");
			}
			else if (EndDatePicker.SelectedDate.Value > StartDatePicker.SelectedDate.Value.AddMonths(this.MaxDuration))
			{
				throw new ApplicationException("選擇的時間區間大于" + this.MaxDuration + "天，請重新選擇！");
			}
			return EndDatePicker.SelectedDate.Value.ToString("yyyyMMdd");
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
			DbAccessHelper database = new DbAccessHelper("InnoluxDB");
			string shiftDate = database.ExecuteScalar("SELECT T.SHIFT_DATE FROM SHIFT_DATE T WHERE SYSDATE BETWEEN T.CALSTARTDATE AND T.CALENDDATE").ToString();
			string year = shiftDate.Substring(0, 4);
			string month = shiftDate.Substring(4, 2);
			string day = shiftDate.Substring(6, 2);
			this.EndDatePicker.SelectedDate = Convert.ToDateTime(year + "-" + month + "-" + day);
			this.StartDatePicker.SelectedDate = Convert.ToDateTime(year + "-" + month + "-1").AddDays(-3);
		}
	}
}
