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

public partial class CommonForm_UserControl_DateTimePicker_MonthPicker : System.Web.UI.UserControl
{
	public string StartDateTime
	{
		get { return this.ddlStartYear.SelectedValue + this.ddlStartMonth.SelectedValue; }
	}

	public string EndDateTime
	{
		get {

			if (this.StartDateTime.CompareTo(this.ddlEndYear.SelectedValue + this.ddlEndMonth.SelectedValue) > 0)
			{
				throw new ApplicationException("开始月份大于结束月份,请重新选择!");
			}

			return this.ddlEndYear.SelectedValue + this.ddlEndMonth.SelectedValue; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
			DataBindAndSetDefault();
		}
	}

	private void DataBindAndSetDefault()
	{
		// Bind Year dropdownlist
		DbAccessHelper database = new DbAccessHelper("InnoluxDB");
		DataTable source = database.ExecuteDataSet("SELECT DISTINCT YEAR FROM SHIFT_DATE ORDER BY YEAR").Tables[0];
		this.ddlStartYear.DataTextField = "YEAR";
		this.ddlStartYear.DataValueField = "YEAR";
		this.ddlEndYear.DataTextField = "YEAR";
		this.ddlEndYear.DataValueField = "YEAR";
		this.ddlEndYear.DataSource = this.ddlStartYear.DataSource = source;
		this.ddlStartYear.DataBind();
		this.ddlEndYear.DataBind();
		// Set default value
		DateTime current = DateTime.Now;
		this.ddlStartYear.Text = current.AddYears(-1).Year.ToString();
		this.ddlStartMonth.Text = "10";
		this.ddlEndYear.Text = current.Year.ToString();
		this.ddlEndMonth.Text = current.AddMonths(-1).Month.ToString();
	}
}
