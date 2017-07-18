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
using System.ComponentModel;

public delegate void ClickEventHandler(object sender, EventArgs e);

[ValidationPropertyAttribute("SelectedListItems")]
public partial class CommonForm_UserControl_CFTStepIdSelector : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        //this.TextBox1.Enabled = this.Enable; 
        this.CfStepOpenImg.Visible = this.Enable;
        if (this.SetDefault == true)
        {
            if (this.SelectedIndex == 0) // IF
                this.TextBox1.Text = "'2510_I1','2510_I2','2510_I3','2510_I4'";
            else if (this.SelectedIndex == 1)
                this.TextBox1.Text = "'2910_I1','2910_I2','2910_I3','2910_I4'";
        }
    } 

    public event ClickEventHandler Click;
    protected void OnClick(EventArgs e)
    {
        if (Click != null)
            Click(this, e);
    }
    protected void OkButton_Click(object sender, ImageClickEventArgs e)
    {
        string temp = "";
        foreach (ListItem item in this.lsbRight.Items)
        {
            temp += "'" + item.Value + "',";
        }
        if (!string.IsNullOrEmpty(temp))
        {
            temp = temp.Substring(0, temp.Length - 1);
        }
        this.TextBox1.Text = temp;
        this.TextBox1.ToolTip = temp;
        this.ModalPopupExtender.Hide();

        this.OnClick(e);
    }

    #region Public members
    public Unit TextBoxWidth
    {
        get
        {
            object obj2 = this.ViewState["Width"];
            if (obj2 != null)
            {
                return (Unit)obj2;
            }
            return Unit.Parse("150");
        }
        set
        {
            if (value.Value < 0.0)
            {
                throw new ArgumentOutOfRangeException("Width");
            }
            this.ViewState["Width"] = value;
        }
    }

    public string ValidationGroup
    {
        get
        {
            object obj2 = this.ViewState["ValidationGroup"];
            if (obj2 != null)
            {
                return (string)obj2;
            }
            return string.Empty;
        }
        set
        {
            this.ViewState["ValidationGroup"] = value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            object obj2 = this.ViewState["ErrorMessage"];
            if (obj2 != null)
            {
                return (string)obj2;
            }
            return "*";
        }
        set
        {
            this.ViewState["ErrorMessage"] = value;
        }
    }

	public ListItemCollection SelectedListItems
	{
		get
		{
			return this.lsbRight.Items;
		}
	}

	public string SelectedStepIds
	{
		get { return this.TextBox1.Text; }
	}

	public string SelectedProcessName
	{
		get 
        {
            if (this.TextBox1.Text.Contains("2510"))
                return "IF";
            else if (this.TextBox1.Text.Contains("2910"))
                return "IS";
            return string.Empty;
            //return this.ddlGroup.SelectedItem.Text; 
        }
	}

    // Todo: July 15, 2009 - by steven
    public bool Enable
    {
        get 
        {
            if (ViewState["Enable"] == null)
                return true;
            return bool.Parse(ViewState["Enable"].ToString());
        }
        set { ViewState["Enable"] = value; }
    }
    public bool SetDefault
    {
        get 
        {
            if (ViewState["SetDefault"] == null)
                return false;
            return bool.Parse(ViewState["SetDefault"].ToString());
        }
        set { ViewState["SetDefault"] = value; }
    }

    public int SelectedIndex
    {
        get
        {
            if (ViewState["SelectedIndex"] == null)
                return 0;
            return int.Parse(ViewState["SelectedIndex"].ToString());
        }
        set { ViewState["SelectedIndex"] = value; }
    }
    #endregion

    #region Even
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.TextBox1.Width = this.TextBoxWidth;
            this.RequiredFieldValidator1.ValidationGroup = this.ValidationGroup;
            this.RequiredFieldValidator1.ErrorMessage = this.ErrorMessage;

            // Todo: default select IF
            if (this.ddlGroup.Items.Count > 0 && this.ddlGroup.Items.Count >= this.SelectedIndex)
            {
                this.ddlGroup.SelectedIndex = this.SelectedIndex;
                this.ddlGroup_SelectedIndexChanged(sender, e);
            }
        }

        ////this.TextBox1.Enabled = this.Enable; 
        //this.CfStepOpenImg.Visible = this.Enable;
        //if (this.SetDefault == true)
        //{
        //    if (this.SelectedIndex == 0) // IF
        //        this.TextBox1.Text = "'2510_I1','2510_I2','2510_I3','2510_I4'";
        //    else if (this.SelectedIndex == 1)
        //        this.TextBox1.Text = "'2910_I1','2910_I2','2910_I3','2910_I4'";
        //}
    }

	protected void OkButton_Click(object sender, EventArgs e)
	{
        //string temp = "";
        //foreach (ListItem item in this.lsbRight.Items)
        //{
        //    temp += "'" + item.Value + "',";
        //}
        //if (!string.IsNullOrEmpty(temp))
        //{
        //    temp = temp.Substring(0, temp.Length - 1);
        //}
        //this.TextBox1.Text = temp;
        //this.TextBox1.ToolTip = temp;
        //this.ModalPopupExtender.Hide();
	}

	protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.lsbLeft.Items.Clear();
		this.lsbRight.Items.Clear();
		ListItemCollection data = new ListItemCollection();
		string text = this.ddlGroup.SelectedItem.Text;
		string value = this.ddlGroup.SelectedItem.Value;

		if (this.ddlGroup.SelectedItem.Text == "IF")
		{
			data.Add(new ListItem(value + "_I1", value + "_I1"));
            data.Add(new ListItem(value + "_I2", value + "_I2"));
			data.Add(new ListItem(value + "_I3", value + "_I3"));
			data.Add(new ListItem(value + "_I4", value + "_I4"));
		}
		if (this.ddlGroup.SelectedItem.Text == "IS")
		{
            data.Add(new ListItem(value + "_I1", value + "_I1"));
			data.Add(new ListItem(value + "_I2", value + "_I2"));
			data.Add(new ListItem(value + "_I3", value + "_I3"));
			data.Add(new ListItem(value + "_I4", value + "_I4"));
		}

		this.lsbLeft.DataSource = data;
		this.lsbLeft.DataBind();
    }
    #endregion
}
