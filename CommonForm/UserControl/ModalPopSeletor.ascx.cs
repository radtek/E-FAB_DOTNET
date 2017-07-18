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

[ValidationPropertyAttribute("SelectedListItems")]
public partial class Fabinfo_CF_Report_CF_Defect_Trend_ModalPopSeletor : System.Web.UI.UserControl
{
    #region Public members
    public Unit Width
    {
        get
        {
            object obj2 = this.ViewState["Width"];
            if (obj2 != null)
            {
                return (Unit)obj2;
            }
            return Unit.Empty;
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

    public int Rows
    {
        get
        {
            object obj2 = this.ViewState["Rows"];
            if (obj2 != null)
            {
                return (int)obj2;
            }
            return 4;
        }
        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException("value");
            }
            this.ViewState["Rows"] = value;
        }
    }

    // Todo: 
    public bool Enable
    {
        get 
        {
            string temp = string.Format("{0}", ViewState["Enable"]);
            if (string.IsNullOrEmpty(temp) == false)
                return bool.Parse(temp);
            return true;
        }
        set
        {
            ViewState["Enable"] = value;
            if (this.TextBox1 != null)
                this.TextBox1.Enabled = this.ModalPopOpenImg.Visible = value;
        }
    }

    public DropDownList DropdownList
    {
        get { return this.ddlGroup; }
    }

    public ListBox LeftListBox
    {
        get { return this.lsbLeft; }
    }

    public ListBox RightListBox
    {
        get { return this.lsbRight; }
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
            return string.Empty;
        }
        set
        {
            this.ViewState["ErrorMessage"] = value;
        }
    }

    public string GroupLabelText
    {
        get { return this.lblGroup.Text; }
        set { this.lblGroup.Text = value; }
    }

    public ListItemCollection SelectedListItems
    {
        get
        {
            return this.lsbRight.Items;
        }
    }

    public bool ShowDropdownSelector
    {
        get { return this.tdDropdown.Visible; }
        set { this.tdDropdown.Visible = value; }
    }

	//public string Title
	//{
	//    get { return this.modalPanel.Title; }
	//    set { this.modalPanel.Title = value; } 
	//}
    #endregion

    private readonly static object dropDownListSelectedIndexChangedKey = new object();
    public event EventHandler DropDownListSelectedIndexChanged
    {
        add
        {
            this.Events.AddHandler(dropDownListSelectedIndexChangedKey, value);
        }
        remove
        {
            this.Events.RemoveHandler(dropDownListSelectedIndexChangedKey, value);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.RequiredFieldValidator1.ValidationGroup = this.ValidationGroup;
            this.RequiredFieldValidator1.ErrorMessage = this.ErrorMessage;

            this.TextBox1.Enabled = this.ModalPopOpenImg.Visible = this.Enable;
        }
    }

    public void DropdownListDataBind(object dataSource, string dataTextField, string dataValueField)
    {
        this.ddlGroup.Items.Clear();
        ListItem item = new ListItem("-select-", "-1");
        this.ddlGroup.Items.Add(item);
        this.ddlGroup.DataTextField = dataTextField;
        this.ddlGroup.DataValueField = dataValueField;
        this.ddlGroup.DataSource = dataSource;
        this.ddlGroup.DataBind();
    }

    public void LeftListBoxDataBind(object dataSource, string dataTextField, string dataValueField)
    {
        this.lsbLeft.Items.Clear();
        if (dataSource != null)
        {
            this.lsbLeft.DataSource = dataSource;
            this.lsbLeft.DataTextField = dataTextField;
            this.lsbLeft.DataValueField = dataValueField;
            this.lsbLeft.DataBind();
        }
        //else
        //{
        //    this.lsbLeft.Items.Clear();
        //}
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        string temp = "";
        foreach (ListItem item in this.RightListBox.Items)
        {
            temp += item.Text + ",";
        }
        this.TextBox1.Text = temp;
        this.TextBox1.ToolTip = temp;
        this.ModalPopOpenImg.ToolTip = temp;
        this.ModalPopupExtender.Hide();

        this.UpdatePanel3.Update();
    }

    protected void btnToRight_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.LeftListBox.Items.Count; i++)
        {
            if (this.LeftListBox.Items[i].Selected && !this.RightListBox.Items.Contains(this.LeftListBox.Items[i]))
            {
                this.LeftListBox.Items[i].Selected = false;
                this.RightListBox.Items.Add(this.LeftListBox.Items[i]);
            }
        }
    }

    protected void btnToLeft_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.RightListBox.Items.Count; i++)
        {
            if (this.RightListBox.Items[i].Selected)
            {
                this.RightListBox.Items.Remove(this.RightListBox.Items[i]);
            }
        }
    }

    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        this.RightListBox.Items.Clear();
    }

    protected void btnAddAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.LeftListBox.Items.Count; i++)
        {
            this.LeftListBox.Items[i].Selected = false;
            this.RightListBox.Items.Add(this.LeftListBox.Items[i]);
        }
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        EventHandler handler = (EventHandler)this.Events[dropDownListSelectedIndexChangedKey];
        if (handler != null)
        {
            handler(sender, e);
        }
    }

    public void ClearAll()
    {
        this.TextBox1.Text = string.Empty;
        this.ModalPopOpenImg.ToolTip = string.Empty;
        this.TextBox1.ToolTip = string.Empty;
        //this.ddlGroup.DataSource = null;
        this.RightListBox.Items.Clear();
        this.LeftListBox.Items.Clear();
    }
}
