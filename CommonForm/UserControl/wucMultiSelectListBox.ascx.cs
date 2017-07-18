using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.Collections.Generic;

public partial class UserControls_MultiSelectListBox : System.Web.UI.UserControl
{
    /// <summary>
    /// this first column
    /// </summary>
    public DataTable LeftDataSource;// { get; set; }
    public DataTable RightDataSource;// { get; set; }
    public string SelectTitle
    {
        get { return this.SelectorTxt.Text; }
        set { SelectorTxt.Text = value; }
    }

    public double TextWidth
    {
        get { return this.selectedTxt.Width.Value; }
        set { this.selectedTxt.Width = (Unit)value; }
    }
    public void CleanSelectedText()
    {
        selectedTxt.Text = String.Empty;
    }
    public void SetMultiSelectEnable(bool boolflag)
    {
        ModalPopOpenImg.Enabled = boolflag;
        selectedTxt.Enabled = boolflag;
        Color c =ModalPopOpenImg.BackColor;
        if (!boolflag)
        {
            ModalPopOpenImg.BackColor = Color.Gainsboro;
            selectedTxt.BackColor = Color.Silver;
        }
        else
        {
            ModalPopOpenImg.BackColor = Color.FromName("0");
            selectedTxt.BackColor = Color.FromName("0");
        }
    }
    public string SelectedValues
    {
        get
        {
            string SelectedStringValues="";
            foreach (ListItem item in this.lbxTo.Items)
                SelectedStringValues+=string.Format(",{0}", item.Value);
            if (SelectedStringValues.Length > 0)
                SelectedStringValues = SelectedStringValues.Substring(1);
            return SelectedStringValues;
        }
    }
    public string SelectedTexts
    {
        get
        {
            string SelectedStringTexts = "";
            foreach (ListItem item in this.lbxTo.Items)
                SelectedStringTexts += string.Format(",{0}", item.Text);
            if (SelectedStringTexts.Length > 0)
                SelectedStringTexts = SelectedStringTexts.Substring(1);
            return SelectedStringTexts;
        }
        set 
        {
            selectedTxt.Text = value; 
            
        }
    }
    public ListItemCollection LeftListItem
    {
        get
        {
            return this.lbxSource.Items;
        }
    }
    public ListItemCollection RightListItem
    {
        get
        {
            return this.lbxTo.Items;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (this.LeftDataSource != null && this.LeftDataSource.Rows.Count > 0)
            {
                this.lbxSource.DataSource = this.LeftDataSource;
                this.lbxSource.DataValueField = this.lbxSource.DataTextField = this.LeftDataSource.Columns[0].ColumnName;
                if (this.LeftDataSource.Columns.Count > 1)
                    this.lbxSource.DataTextField = this.LeftDataSource.Columns[1].ColumnName;
                this.lbxSource.DataBind();
            }
            if (this.RightDataSource != null && this.RightDataSource.Rows.Count > 0)
            {
                this.lbxTo.DataSource = this.RightDataSource;
                this.lbxTo.DataValueField = this.lbxTo.DataTextField = this.RightDataSource.Columns[0].ColumnName;
                if (this.RightDataSource.Columns.Count > 1)
                    this.lbxTo.DataTextField = this.RightDataSource.Columns[1].ColumnName;
                this.lbxTo.DataBind();
            }
        }
    }
    public void BindLeftList(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            this.lbxSource.DataSource = dt;
            this.lbxSource.DataValueField = this.lbxSource.DataTextField = dt.Columns[0].ColumnName;
            if (dt.Columns.Count > 1)
                this.lbxSource.DataTextField = dt.Columns[1].ColumnName;
            this.lbxSource.DataBind();
            this.lbxTo.Items.Clear();
        }
    }

    protected void listBoxOperate_OnCommand(object sender, CommandEventArgs e)
    {
        ArrayList arrTo = new ArrayList();
        switch (e.CommandName)
        {
            case "ToRight":
                if (this.lbxSource.SelectedIndex == -1)
                    break;
                else
                {
                    foreach (ListItem item in lbxSource.Items)
                        if (item.Selected)
                            arrTo.Add(item);

                    foreach (ListItem item in arrTo)
                    {
                        this.lbxTo.Items.Add(item);
                        this.lbxSource.Items.Remove(item);
                    }

                    break;
                }

            case "AllToRight":
                if (this.lbxSource.Items.Count > 0)
                {
                    foreach (ListItem item in lbxSource.Items)
                        this.lbxTo.Items.Add(item);
                    this.lbxSource.Items.Clear();
                    break;
                }
                else
                    break;
            case "ToLeft":
                if (this.lbxTo.SelectedIndex == -1)
                    break;
                else
                {
                    foreach (ListItem item in lbxTo.Items)
                        if (item.Selected)
                            arrTo.Add(item);

                    foreach (ListItem item in arrTo)
                    {
                        this.lbxSource.Items.Add(item);
                        this.lbxTo.Items.Remove(item);
                    }

                    break;
                }
                break;
            case "AllToLeft":
                if (this.lbxTo.Items.Count > 0)
                {
                    foreach (ListItem item in lbxTo.Items)
                        this.lbxSource.Items.Add(item);
                    this.lbxTo.Items.Clear();
                    break;
                }
                else
                    break;
            case "ToTop":
                int top = -1;
                for (int i = 0; i < lbxTo.Items.Count; i++)
                //for (int i = lbxTo.Items.Count - 1; i > -1; i--)
                {
                    
                    if (lbxTo.Items[i].Selected)
                    {
                        top += 1;
                        if (i > 0 && lbxTo.SelectedIndex > -1)
                        {
                            ListItem item = lbxTo.Items[i];
                            lbxTo.Items.RemoveAt(i);
                            lbxTo.Items.Insert(0 + top, item);
                        }
                    }
                }
                break;
            case "ToUp":
                for (int i = 0; i < lbxTo.Items.Count; i++)
                {
                    if (lbxTo.Items[i].Selected)
                    {
                        if (i > 0 && lbxTo.SelectedIndex > -1)
                        {
                            ListItem item = lbxTo.Items[i];
                            lbxTo.Items.RemoveAt(i);
                            lbxTo.Items.Insert(i - 1, item);
                        }
                    }
                }
                break;
            case "ToDown":
                for (int i = lbxTo.Items.Count - 1; i > -1; i--)
                {
                    if (lbxTo.Items[i].Selected)
                    {
                        ListItem item = lbxTo.Items[i];
                        if (i < lbxTo.Items.Count - 1)
                        {
                            lbxTo.Items.Insert(i + 2, item);
                            lbxTo.Items.RemoveAt(i);
                        }

                    }
                }
                break;
            case "ToBottom":
                int down = -1;
                for (int i = lbxTo.Items.Count - 1; i > -1; i--)
                {
                    if (lbxTo.Items[i].Selected)
                    {
                        down += 1;
                        ListItem item = lbxTo.Items[i];
                        if (i < lbxTo.Items.Count - 1)
                        {
                            lbxTo.Items.Insert(lbxTo.Items.Count - down, item);
                            lbxTo.Items.RemoveAt(i);
                        }

                    }
                }
                break;
        }
    }
    protected void OkButton_Click(object sender, ImageClickEventArgs e)
    {
        selectedTxt.Text = this.SelectedTexts;
        selectedTxt.ToolTip = this.SelectedTexts;
        this.ModalPopupExtender.Hide();
    }
}
