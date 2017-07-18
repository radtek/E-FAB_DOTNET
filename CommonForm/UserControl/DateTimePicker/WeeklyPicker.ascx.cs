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

public partial class CommonForm_UserControl_DateTimePicker_WeeklyPicker : System.Web.UI.UserControl
{
	#region Public members
	public string StartDateTime
	{
		get
		{
			CheckSelectedValue();
			return this.ddlStartWeek.SelectedValue;
		}
	}

	public string ShiftStartDateTime
	{
		get
		{
			CheckSelectedValue();
			return this.ddlStartYear.Text.Trim() + "W" + this.ddlStartWeek.SelectedItem.Text.Trim();
		}
	}

	public string EndDateTime
	{
		get
		{
			CheckSelectedValue();
			return this.ddlEndWeek.SelectedValue;
		}
	}

	public string ShiftEndDateTime
	{
		get
		{
			CheckSelectedValue();
			return this.ddlEndYear.Text.Trim() + "W" + this.ddlEndWeek.SelectedItem.Text.Trim();
		}
	}

	public int MaxDuration 
	{
		get
		{
			object obj = this.ViewState["MaxDuration"];
			return obj == null ? -1 : (int)obj;
		}
		set { this.ViewState["MaxDuration"] = value; }
	}
	#endregion

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
            //DatabindDefaultValue();
            this.SetDefaultValue();
		}
	}

	#region Private methods
	private void CheckSelectedValue()
	{
		if (this.MaxDuration > 0)
		{
			int sy = int.Parse(this.ddlStartYear.Text);
			int sw = int.Parse(this.ddlStartWeek.SelectedItem.Text);
			int ey = int.Parse(this.ddlEndYear.Text);
			int ew = int.Parse(this.ddlEndWeek.SelectedItem.Text);
			int offsize = (ey - sy) * 53 + (ew - sw) + 1;
            // Todo: the value is real or so 
            int max = 365 / 12 * this.MaxDuration / 7; 
            //if (offsize > this.MaxDuration)
            if (offsize > max)
			{
				throw new ApplicationException("Duration over " + this.MaxDuration + " months, please select again !");
			}
			else if (offsize < 0)
			{
				throw new ApplicationException("The start value more then the end value, please select again !");
			}
		}
    }

    private void SetDefaultValue()
    {
        string sql = @"
with X as (select max(to_number(year)) as max_yr, min(to_number(year)) as min_yr from shift_date ), 
Y as
(   select max(to_number(td_week)) - 2 as start_wk, year as start_yr, max(to_number(td_week)) as start_max_wk 
    from shift_date 
    where (year + 1) = to_char(sysdate, 'yyyy')  
    group by year
),
Z as
(   select year as end_yr, td_week as end_wk, shift_date as end_dt, m.end_max_wk as end_max_wk
    from shift_date,
    ( select max(to_number(td_week)) as end_max_wk 
      from shift_date 
      where year = to_char(sysdate, 'yyyy') ) m 
    where sysdate between calstartdate and calenddate
)

select  Y.start_yr, Y.start_wk, Y.start_max_wk, Z.end_dt, Z.end_yr, Z.end_max_wk, Z.end_wk, X.max_yr, X.min_yr
from X, Y, Z
";
        DbAccessHelper database = new DbAccessHelper("InnoluxDB");
        DataTable dt = database.ExecuteDataSet(sql).Tables[0];
        if (dt == null || dt.Rows.Count < 1)
            return;
        int minYR = Utils.ConvertObject<int>(dt.Rows[0]["MIN_YR"]);
        int maxYR = Utils.ConvertObject<int>(dt.Rows[0]["MAX_YR"]);
        int startMaxWeek = Utils.ConvertObject<int>(dt.Rows[0]["START_MAX_WK"]);
        int endMaxWeek = Utils.ConvertObject<int>(dt.Rows[0]["END_MAX_WK"]);
        int endDefaultWeek = Utils.ConvertObject<int>(dt.Rows[0]["END_WK"]);
        
        this.BindToComobox(this.ddlStartYear, minYR, maxYR, DateTime.Now.Year - 1);
        this.BindToComobox(this.ddlEndYear, minYR, maxYR, DateTime.Now.Year);

        this.BindToComobox(this.ddlStartWeek, 1, startMaxWeek, startMaxWeek - 2);
        this.BindToComobox(this.ddlEndWeek, 1, endMaxWeek, endDefaultWeek);
    }

    private void BindToComobox(DropDownList ddl, int minValue, int maxValue, int defaultValue)
    {
        ddl.Items.Clear();
        for (int i = minValue; i <= maxValue; i++)
        {
            ListItem item = new ListItem(i.ToString("00"), i.ToString("00"));
            ddl.Items.Add(item);
        }
        if (defaultValue > 0)
            ddl.SelectedValue = defaultValue.ToString("00");
    }


    #region Old code, shoule be delete
    private void DatabindDefaultValue()
	{
		#region SQL
		string sql = @" SELECT MIN(X.SHIFT_DATE) AS START_DT,
										MAX(Y.MIN_YR) AS START_YR,
										MAX(Y.MIN_WK) AS START_WK,
										MAX(X.SHIFT_DATE) AS END_DT,
										MAX(Y.MAX_YR) AS END_YR,
										MAX(Y.MAX_WK) AS END_WK,
										MIN(Z.MIN_YR) AS MIN_YR,
										MAX(Z.MAX_YR) AS MAX_YR
								   FROM SHIFT_DATE X,
										(SELECT MIN(YEAR) AS MIN_YR,
												MAX(TD_WEEK) - 2 AS MIN_WK,
												MAX(YEAR) AS MAX_YR,
												SUBSTR(MAX(YEAR || TD_WEEK)-1, 5, 2) AS MAX_WK
										   FROM SHIFT_DATE T
										  WHERE SYSDATE BETWEEN T.CALSTARTDATE AND ADD_MONTHS(T.CALENDDATE, 12)
										  ORDER BY SHIFT_DATE) Y,
										(SELECT MAX(YEAR) AS MAX_YR, MIN(YEAR) AS MIN_YR FROM SHIFT_DATE) Z
								  WHERE X.YEAR || X.TD_WEEK = Y.MIN_YR || Y.MIN_WK
									 OR X.YEAR || X.TD_WEEK = Y.MAX_YR || Y.MAX_WK";
		#endregion

		DbAccessHelper database = new DbAccessHelper("InnoluxDB");
		using (IDataReader reader = database.ExecuteReader(sql))
		{
			if (reader.Read())
			{
				int minYR = Utils.ConvertObject<int>(reader["MIN_YR"]);
				int maxYR = Utils.ConvertObject<int>(reader["MAX_YR"]);
				this.DataBindDdlYear(minYR, maxYR);
				
				this.ddlStartYear.Text = Utils.ConvertObject<string>(reader["START_YR"]);
				this.DataBindWeekDdl(this.ddlStartWeek, this.ddlStartYear.Text, true);
				
				this.ddlEndYear.Text = Utils.ConvertObject<string>(reader["END_YR"]);
				this.DataBindWeekDdl(this.ddlEndWeek, this.ddlEndYear.Text, false);

				this.ddlStartWeek.Text = Utils.ConvertObject<string>(reader["START_DT"]);
				this.ddlEndWeek.Text = Utils.ConvertObject<string>(reader["END_DT"]);
			}
		}
	}

	private void DataBindDdlYear(int minYR, int maxYR)
	{
		ListItemCollection source = new ListItemCollection();
		for (int i = minYR; i <= maxYR; i++)
		{ 
			string s = i.ToString();
			source.Add(new ListItem(s, s));
		}
		this.ddlStartYear.DataSource = source;
		this.ddlStartYear.DataBind();
		this.ddlEndYear.DataSource = source;
		this.ddlEndYear.DataBind();
	}

	private void DataBindWeekDdl(DropDownList ddl, string year, bool startOrEnd)
	{
		DbAccessHelper database = new DbAccessHelper("InnoluxDB");
        string fmSql = @"SELECT TO_CHAR(T.TD_WEEK, '00') as TD_WEEK, MIN(T.SHIFT_DATE) START_DATE, MAX(T.SHIFT_DATE) END_DATE
											  FROM SHIFT_DATE T
											 WHERE T.YEAR = {0}
											   AND T.TD_WEEK IS NOT NULL
											 GROUP BY T.TD_WEEK
											 ORDER BY TO_CHAR(T.TD_WEEK, '00')";
		ddl.DataTextField = "TD_WEEK";
		ddl.DataValueField = startOrEnd ? "START_DATE" : "END_DATE";
		ddl.DataSource = database.ExecuteDataSet(string.Format(fmSql, year)).Tables[0];
		ddl.DataBind();
    }
    #endregion

    private void ShowMessage(string msg)
	{
		ScriptManager.RegisterClientScriptBlock(
			this.Page,
			this.Page.GetType(),
			"alertMes",
			string.Format(@"javascript:alert('{0}');", msg),
			true);
	}

	private int GetDdlIndex(DropDownList ddl, string text)
	{
		for (int i = 0; i < ddl.Items.Count; i++)
		{
			if (ddl.Items[i].Text == text)
			{
				return i;
			}
		}
		return 0;
	}
	#endregion
	protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
	{
		DropDownList ddl = sender as DropDownList;
		if (ddl.ID == this.ddlStartYear.ID)
		{
			this.DataBindWeekDdl(this.ddlStartWeek, this.ddlStartYear.Text, true);
		}
		else
		{
			this.DataBindWeekDdl(this.ddlEndWeek, this.ddlEndYear.Text, false);
		}
	}
}
