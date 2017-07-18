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
public partial class CommonForm_UserControl_AryStepIdSelector : System.Web.UI.UserControl
{
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

    public int DefaultSelectedIndex
    {
        get
        {
            if (this.ViewState["DefaultSelectedIndex"] == null)
                return 0;
            return int.Parse(this.ViewState["DefaultSelectedIndex"].ToString());
        }
        set { this.ViewState["DefaultSelectedIndex"] = value; }
    }
    public bool IsSingleSelect
    {
        get
        {
            if (this.ViewState["IsSingleSelect"] == null)
                return false;
            return bool.Parse(this.ViewState["IsSingleSelect"].ToString());
        }
        set { this.ViewState["IsSingleSelect"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.TextBox1.Width = this.TextBoxWidth;
            this.RequiredFieldValidator1.ValidationGroup = this.ValidationGroup;
            this.RequiredFieldValidator1.ErrorMessage = this.ErrorMessage;
            
            this.ddlGroup_SelectedIndexChanged(sender, e);
        }
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        string temp = "";
        foreach (ListItem item in this.lsbRight.Items)
        {
            temp += item.Text + ",";
        }
        this.TextBox1.Text = temp;
        this.TextBox1.ToolTip = temp;
        this.modalPanel.Hide();
    }

    protected void btnToRight_Click(object sender, EventArgs e)
    {
        AddItemFromSourceListBox(lsbLeft, lsbRight);
    }

    protected void btnToLeft_Click(object sender, EventArgs e)
    {
        RemoveSelectedItem(lsbRight);
    }

    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        this.lsbRight.Items.Clear();
    }

    protected void btnAddAll_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in lsbLeft.Items)
        {
            if (!lsbRight.Items.Contains(item))
            {
                lsbRight.Items.Add(item);
                item.Selected = false;
            }
        }
    }


    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lsbLeft.Items.Clear();
        this.lsbRight.Items.Clear();
        ListItemCollection data = new ListItemCollection();

        if (this.ddlGroup.SelectedIndex == 0) // "NG"
        {
            data.Add(new ListItem("1920", "1920"));
            data.Add(new ListItem("1930", "1930"));
            data.Add(new ListItem("1970", "1970"));
        }
        if (this.ddlGroup.SelectedIndex == 1) // "LR+NG"
        {
            data.Add(new ListItem("1185", "1185"));
            data.Add(new ListItem("1365S", "1365S"));
            data.Add(new ListItem("1378B", "1378B"));
            data.Add(new ListItem("1930", "1930"));
            data.Add(new ListItem("1940", "1940"));
            data.Add(new ListItem("1970", "1970"));
        }

        this.lsbLeft.DataSource = data;
        this.lsbLeft.DataBind();
    }

    private void AddItemFromSourceListBox(ListBox sourceBox, ListBox targetBox)
    {
        foreach (ListItem item in sourceBox.Items)
        {
            if (item.Selected == true && !targetBox.Items.Contains(item))
            {
                if (this.IsSingleSelect == true)
                    targetBox.Items.Clear();
                targetBox.Items.Add(item);
                item.Selected = false;
            }
        }
    }

    private void RemoveSelectedItem(ListBox listControl)
    {
        while (listControl.SelectedIndex != -1)
        {
            listControl.Items.RemoveAt(listControl.SelectedIndex);
        }
    }


    #region method 
    public void SetDefaultSelect(int selectedIndex, bool isEnableGroup)
    {
        this.TextBox1.Text = "";
        this.UpdatePanel3.Update();

        if (selectedIndex < 0 || selectedIndex > this.ddlGroup.Items.Count)
            selectedIndex = 0;

        this.ddlGroup.SelectedIndex = selectedIndex;
        this.ddlGroup.Enabled = isEnableGroup;
        this.ddlGroup_SelectedIndexChanged(null, null);
        this.UpdatePanel4.Update();

        this.lsbRight.Items.Clear();
        this.UpdatePanel2.Update();
    }

    #endregion

}