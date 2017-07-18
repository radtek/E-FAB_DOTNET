using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Innolux.Portal.Common;
using Innolux.Portal.Common.Report.Scheduler;
using Innolux.Portal.EntLibBlock;
using Innolux.Portal.EntLibBlock.DataAccess;
using System.Data.Common;

public partial class CommonForm_UserControl_ReportScheduleSetting : System.Web.UI.UserControl
{
    #region Events
    private readonly static object scheduleSettingOpenKey = new object();
    public event EventHandler ScheduleSettingOpen
    {
        add
        {
            this.Events.AddHandler(scheduleSettingOpenKey, value);
        }
        remove
        {
            this.Events.RemoveHandler(scheduleSettingOpenKey, value);
        }
    }
    #endregion

    #region Public members

	public bool CausesValidation
	{
		get
		{
			return this.btnOpenModalPanel.CausesValidation;
		}
		set {
			this.btnOpenModalPanel.CausesValidation = value;
		}
	}

	public string ValidationGroup
	{
		get
		{
			return this.btnOpenModalPanel.ValidationGroup;
		}
		set
		{
			this.btnOpenModalPanel.ValidationGroup = value;
		}
	}

    public string ReportType
    {
        get
        {
            object obj2 = this.ViewState["ReportType"];
            if (obj2 != null)
            {
                return (string)obj2;
            }
            else
            {
                return null;
            }
        }
        set
        {
            this.ViewState["ReportType"] = value;
        }
    }

    public string UserID
    {
        get { return Innolux.Portal.Security.UserInfo.Current.EmployeeNumber; }
    }

    public string SelectedFrequency
    {
        get
        {
            return (this.triggerTypeTabStrip.SelectedTab.Text);
        }
    }

    public OutputAvailability Availability
    {
        get
        {
            return Utils.ConvertObject<OutputAvailability>(this.rbtnOutputAvail.SelectedValue);
        }
    }

    public PastDateTimeType PastType
    {
        get
        {
            return Utils.ConvertObject<PastDateTimeType>(this.ddlPastType.SelectedValue);
        }
    }

    public int PastUnit
    {
        get
        {
            return Utils.ConvertObject<int>(this.txtPastUnit.Text);
        }
    }

    public Hashtable Parameters
    {
        get
        {
			object obj = this.ViewState["TriggerParameters"];
            if (obj != null)
            {
                return (Hashtable)obj;
            }
            Hashtable parameters = new Hashtable();

			this.ViewState["TriggerParameters"] = parameters;

            return parameters;
        }
        set
        {
			this.ViewState["TriggerParameters"] = value;
            this.radGridParameters.Rebind();
        }
    }

    public bool EnableEditParameter
    {
        get 
        {
            object obj2 = this.ViewState["EnableEditParameter"];
            return obj2 == null ? false : (bool)obj2;
        }
        set { this.ViewState["EnableEditParameter"] = value; }
    }

	public bool EnableCheckTrigger
	{
		get
		{
			object obj2 = this.ViewState["EnableCheckTrigger"];
			return obj2 == null ? false : (bool)obj2;
		}
		set { this.ViewState["EnableCheckTrigger"] = value; }
	}

	public string CheckTriggerID
	{
		get
		{
			return (this.ckbCheckTrigger.Checked) ? this.ddlCheckTriggerID.Text : string.Empty;
		}
	}

    public bool ShowAllUser
    {
        get
        {
            object obj = this.ViewState["ShowAllUser"];
            return obj == null ? false : (bool)obj;
        }
        set { this.ViewState["ShowAllUser"] = value; }
    }

	public bool ShowOpenButton
	{
		set { this.btnOpenModalPanel.Visible = value; }
	}

    /// <summary>
    /// 控製用戶控件是否顯示。
    /// -1( ?是顯示在?麵上); 
    /// >-1(如果?前用戶擁有值為?前設定的GroupID則顯示，反之隱藏)
    /// </summary>
	public int GroupIDForDisplay
	{
		get
		{
			object obj = this.ViewState["GroupIDForDisplay"];
			return obj == null ? -1 : (int)obj;
		}
		set { this.ViewState["GroupIDForDisplay"] = value; }
	}

	public bool EnableMinutelySetting
	{
		get
		{
			object obj2 = this.ViewState["EnableMinutelySetting"];
			return obj2 == null ? false : (bool)obj2;
		}
		set { this.ViewState["EnableMinutelySetting"] = value; }
	}

	public bool EnableHourlySetting
	{
		get
		{
			object obj2 = this.ViewState["EnableHourlySetting"];
			return obj2 == null ? false : (bool)obj2;
		}
		set { this.ViewState["EnableHourlySetting"] = value; }
	}

    #region Max trigger count

    public int MaxHourlyTriggerCount
    {
        get
        {
            object obj2 = this.ViewState["MaxHourlyTriggerCount"];
            if (obj2 != null)
            {
                return (int)obj2;
            }
            else if (System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"] != null)
            {
                this.ViewState["MaxHourlyTriggerCount"] = Int32.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"]);
                return (int)this.ViewState["MaxHourlyTriggerCount"];
            }
            else
            return 1;
        }
        set
        {
            this.ViewState["MaxHourlyTriggerCount"] = value;
        }
    }

    public int MaxDailyTriggerCount
    {
        get
        {
            object obj2 = this.ViewState["MaxDailyTriggerCount"];
            if (obj2 != null)
            {
                return (int)obj2;
            }
            else if (System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"] != null)
            {
                this.ViewState["MaxDailyTriggerCount"] = Int32.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"]);
                return (int)this.ViewState["MaxDailyTriggerCount"];
            }
            else
                return 1;
        }
        set
        {
            this.ViewState["MaxDailyTriggerCount"] = value;
        }
    }

    public int MaxWeeklyTriggerCount
    {
        get
        {
            object obj2 = this.ViewState["MaxWeeklyTriggerCount"];
            if (obj2 != null)
            {
                return (int)obj2;
            }
            else if (System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"] != null)
            {
                this.ViewState["MaxWeeklyTriggerCount"] = Int32.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"]);
                return (int)this.ViewState["MaxWeeklyTriggerCount"];
            }
            else
                return 1;
        }
        set
        {
            this.ViewState["MaxWeeklyTriggerCount"] = value;
        }
    }

    public int MaxMonthlyTriggerCount
    {
        get
        {
            object obj2 = this.ViewState["MaxMonthlyTriggerCount"];
            if (obj2 != null)
            {
                return (int)obj2;
            }
            else if (System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"] != null)
            {
                this.ViewState["MaxMonthlyTriggerCount"] = Int32.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"]);
                return (int)this.ViewState["MaxMonthlyTriggerCount"];
            }
            else
                return 1;
        }
        set
        {
            this.ViewState["MaxMonthlyTriggerCount"] = value;
        }
    }

	public int MaxMinutelyTriggerCount
	{
		get
		{
			object obj2 = this.ViewState["MaxMinutelyTriggerCount"];
			if (obj2 != null)
			{
				return (int)obj2;
			}
            else if (System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"] != null)
            {
                this.ViewState["MaxMinutelyTriggerCount"] = Int32.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["MaxTriggerCountPerTimeType"]);
                return (int)this.ViewState["MaxMinutelyTriggerCount"];
            }
            else
			    return 1;
		}
		set
		{
			this.ViewState["MaxMinutelyTriggerCount"] = value;
		}
	}


    #endregion

    #endregion

    #region Constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            DataTable dt = Innolux.Portal.Security.UserInfo.Current.MenuRight;
         
            //Z09 means the right to perform schedule settings
            dt.DefaultView.RowFilter = " ITEMNO = 'Z09'";
            if (dt.DefaultView.Count > 0)
                this.Visible = true;
            else
                this.Visible = false;


            //if (this.GroupIDForDisplay > -1)
            //{
            //    this.Visible = Innolux.Portal.Security.UserInfo.Current.CheckGroup(this.GroupIDForDisplay);
            //}
            this.radGridParameters.MasterTableView.CommandItemDisplay = 
				EnableEditParameter ? GridCommandItemDisplay.Bottom : GridCommandItemDisplay.None;
			this.ckbCheckTrigger.Enabled = this.EnableCheckTrigger;
			this.triggerTypeTabStrip.Tabs[0].Enabled = this.EnableMinutelySetting;
			this.MinutelyPageView.Enabled = this.EnableMinutelySetting;
			this.triggerTypeTabStrip.Tabs[1].Enabled = this.EnableHourlySetting;
			this.HourlyPageView.Enabled = this.EnableHourlySetting;
        }
    }
    #endregion

	#region Public methods
	public void Show()
	{
		if (string.IsNullOrEmpty(this.ReportType))
		{
			this.ShowMessage("Report Type does not set.");
			return;
		}

		this.RadGrid2.Rebind();

		this.modalPanel.Show();
	}

	public void Hide()
	{
		this.modalPanel.Hide();
	}
	#endregion

	#region Protected control event methods
	protected void btnOpenModalPanel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.ReportType))
        {
            this.ShowMessage("Report Type does not set.");
            return;
        }

        EventHandler handler = (EventHandler)this.Events[scheduleSettingOpenKey];
        if (handler != null)
        {
            handler(sender, e);
        }
        
        this.modalPanel.Show();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            SchedulableRpt rpt = SchedulableRpt.GetSchedulableRptByRptName(this.ReportType);

            int existTriggerCount = rpt.GetTriggerCount(this.UserID, this.SelectedFrequency);

            int maxTriggerCount;
            Quartz.Trigger trigger = this.GetTrigger(out maxTriggerCount);

            if (existTriggerCount >= maxTriggerCount)
            {
                this.ShowMessage(existTriggerCount + " " + this.SelectedFrequency + " schedule setted. No more allowed.");
            }
            else
            {
                rpt.AddToScheduler(
					trigger, 
					this.UserID, 
					this.SelectedFrequency, 
					this.Parameters, 
					this.Availability, 
					this.PastType, 
					this.PastUnit, 
					this.ckbMonitor.Checked, 
					this.CheckTriggerID);
                this.RadGrid2.Rebind();
                this.ShowMessage("Report schedule trigger set successful");
            }
        }
        catch (Exception ex)
        {
            this.ShowMessage("Add trigger error! \r\n => " + ex.Message);
        }
    }

    #region Parameter rad grid event methods
    protected void RadGridParameters_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        this.radGridParameters.DataSource = Parameters;
    }

    protected void RadGridParameters_InsertCommand(object source, GridCommandEventArgs e)
    {
        bool hasKey = false;
        string key = string.Empty;
        string value = string.Empty;
        GridEditManager editMan = (e.Item as GridEditableItem).EditManager;
        //Set new values
        foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
        {
            if (column is IGridEditableColumn)
            {
                IGridEditableColumn editableCol = (column as IGridEditableColumn);
                if (editableCol.IsEditable)
                {
                    IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);

                    if (editor is GridTextColumnEditor)
                    {
                        if (column.HeaderText.ToUpper() == "KEY")
                        {
                            hasKey = true;
                            key = (editor as GridTextColumnEditor).Text;
                        }
                        if (column.HeaderText.ToUpper() == "VALUE")
                        {
                            value = (editor as GridTextColumnEditor).Text;
                        }
                    }
                }
            }
        }
        if (hasKey)
        {
            this.Parameters.Add(key, value);
        }
    }

    protected void RadGridParameters_DeleteCommand(object source, GridCommandEventArgs e)
    {
        string name = e.Item.Cells[2].Text;
        this.Parameters.Remove(name);
        this.radGridParameters.CurrentPageIndex = 0;
    }
    #endregion

    #region Radgrid 2 event methods
    protected void RadGrid2_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is Telerik.Web.UI.GridDataItem)
        {
        }
    }

    protected void RadGrid2_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "delete")
        {
            string[] temp = e.CommandArgument.ToString().Split('$');
            SchedulerController.Instance.UnScheduleTrigger(temp[0], temp[1]);
        }
		else if (e.CommandName.ToLower() == "execute")
		{
			string[] temp = e.CommandArgument.ToString().Split('$');
			SchedulerController.Instance.ExecuteTriggerImmediately(temp[0], temp[1], null);
		}
		else
		{
			string[] temp = e.CommandArgument.ToString().Split('$');
			if (e.CommandName.ToLower() == "normal")
			{
				SchedulerController.Instance.PauseTrigger(temp[0], temp[1]);
			}
			else if (e.CommandName.ToLower() == "paused")
			{
				SchedulerController.Instance.ResumeTrigger(temp[0], temp[1]);
			}
		}
        this.RadGrid2.Rebind();
    }

    protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        if (!string.IsNullOrEmpty(this.ReportType))
        { 
            SchedulableRpt rpt = SchedulableRpt.GetSchedulableRptByRptName(this.ReportType);
			System.Collections.Generic.List<ReportTriggerDetail> dt = this.ShowAllUser ? rpt.GetTriggers() : rpt.GetTriggers(this.UserID);
			this.RadGrid2.DataSource = dt;
			this.ddlCheckTriggerID.DataSource = dt;
			this.ddlCheckTriggerID.DataValueField = "Name";
			this.ddlCheckTriggerID.DataTextField = "Name";
			this.ddlCheckTriggerID.DataBind();
        }

    }
    #endregion

    #endregion

    #region Private methods
    private Quartz.Trigger GetTrigger(out int maxTriggerCount)
    {
        switch (this.SelectedFrequency.ToLower())
        {
			case "minutely":
				maxTriggerCount = this.MaxMinutelyTriggerCount;
				return this.minutelySetting.GetTrigger();
            case "daily":
                maxTriggerCount = this.MaxDailyTriggerCount;
                return this.dailySetting.GetTrigger();
            case "hourly":
                maxTriggerCount = this.MaxHourlyTriggerCount;
                return this.hourlySetting.GetTrigger();
            case "weekly":
                maxTriggerCount = this.MaxWeeklyTriggerCount;
                return this.weeklySetting.GetTrigger();
            default:
                maxTriggerCount = this.MaxMonthlyTriggerCount;
                return this.monthlySetting.GetTrigger();
        }
    }

    private void ShowMessage(string msg)
    {
		ScriptManager.RegisterClientScriptBlock(
			this.Page,
			this.Page.GetType(),
			"alertMes",
			string.Format(@"javascript:alert('{0}');", msg),
			true);
    }

	/// <summary>
	/// Set isable effect after click to a Button Control
	/// </summary>
	/// <param name="bt">The Button Control</param>
	private void SetBtnClickOnce(Page ownerPage, Button bt, string msg)
	{
		PostBackOptions options = new PostBackOptions(bt, string.Empty);

		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		if (bt.CausesValidation && ownerPage.GetValidators(bt.ValidationGroup).Count > 0)
		{
			options.ClientSubmit = true;
			options.PerformValidation = true;
			options.ValidationGroup = bt.ValidationGroup;

			sb.Append("if (typeof(Page_ClientValidate) == 'function')");
			sb.Append("if(Page_ClientValidate(\"" + bt.ValidationGroup + "\")==false) return false;");
		}

		if (!string.IsNullOrEmpty(bt.PostBackUrl))
		{
			options.ActionUrl = HttpUtility.UrlPathEncode(bt.ResolveClientUrl(bt.PostBackUrl));
		}
		sb.AppendFormat("this.value = '{0}';", msg);
		sb.Append("this.disabled = true;");
		sb.Append(ownerPage.ClientScript.GetPostBackEventReference(options));
		sb.Append(";");
		bt.Attributes.Add("onclick", sb.ToString());
	}
    #endregion
}
